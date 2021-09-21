using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Gaia.Authentication.Jwt;
using Gaia.Helpers.AppSettings;
using Gaia.Helpers.Security;
using Gaia.ToDoList.Business.Interfaces.Notifier;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Interfaces.Services;
using Gaia.ToDoList.Business.Models;
using Gaia.ToDoList.Business.Notifier;
using Gaia.ToDoList.Business.OutputModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gaia.ToDoList.Business.Services
{
    public sealed class AuthenticateService : IAuthenticateService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotifier _notifier;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthenticateService(
            IUserRepository userRepository,
            INotifier notifier,
            IMapper mapper,
            IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _notifier = notifier;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthorizationOutputModel> Authenticate(string login, string password)
        {
            var user = await _userRepository.GetByLogin(login);
            if (user == null)
            {
                _notifier.Handle(new Notification("Invalid login!"));
                return null;
            }

            var encryptedPassword = Encryption.Encrypt(password, AppSettingsHelper.GetValue<string>("Encryption:PassPhrase"));
            if (user.Password != encryptedPassword)
            {
                _notifier.Handle(new Notification("Invalid password!"));
                return null;
            }

            return new AuthorizationOutputModel
            {
                User = _mapper.Map<UserOutputModel>(user),
                Token = CreateAccessToken(user)
            };
        }

        private string CreateAccessToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = _jwtSettings.ValidIn,
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = GetClaimsIdentityFromUser(user),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
            });

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        private static ClaimsIdentity GetClaimsIdentityFromUser(User user)
        {
            var identity = new ClaimsIdentity
            (
                new GenericIdentity(user.Login, "login"),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
                }
            );

            return identity;
        }
    }
}
