﻿using System.Text;
using Gaia.Authentication.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gaia.ToDoList.Api.Configuration.Jwt
{
    public static class JwtConfig
    {
        public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<JwtSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}
