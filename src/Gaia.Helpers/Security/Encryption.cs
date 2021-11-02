using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Gaia.Helpers.Security
{
    public static class Encryption
    {
        private const int KeySize = 256;

        public static string Encrypt(in string plainText, in string passPhrase)
        {
            var initVector = AppSettings.AppSettingsHelper.GetValue<string>("Encryption:InitVector");
            var initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var password = new PasswordDeriveBytes(passPhrase, null);
            var keyBytes = password.GetBytes(KeySize / 8);
            var symmetricKey = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };
            using var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(in string cipherText, in string passPhrase)
        {
            var initVector = AppSettings.AppSettingsHelper.GetValue<string>("Encryption:InitVector");
            var initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            var cipherTextBytes = Convert.FromBase64String(cipherText);
            var password = new PasswordDeriveBytes(passPhrase, null);
            var keyBytes = password.GetBytes(KeySize / 8);
            var symmetricKey = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };
            
            using var descriptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            using var memoryStream = new MemoryStream(cipherTextBytes);
            using var cryptoStream = new CryptoStream(memoryStream, descriptor, CryptoStreamMode.Read);
            
            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
