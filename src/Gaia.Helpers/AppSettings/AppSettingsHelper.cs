using System.IO;
using Microsoft.Extensions.Configuration;

namespace Gaia.Helpers.AppSettings
{
    public static class AppSettingsHelper
    {
        public static T GetValue<T>(in string key, in string jsonFile = "appsettings.json") =>
            GetConfiguration(jsonFile).GetValue<T>(key);

        private static IConfigurationRoot GetConfiguration(in string jsonFile) =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(jsonFile)
                .Build();
    }
}
