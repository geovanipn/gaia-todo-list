using System;
using AutoMapper;
using Gaia.ToDoList.Api.Configuration;
using Gaia.ToDoList.Api.Configuration.DependencyInjection;
using Gaia.ToDoList.Api.Configuration.Swagger;
using Gaia.ToDoList.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gaia.ToDoList.Api
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(hostEnvironment.ContentRootPath)
                   .AddJsonFile("appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                   .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoListDbContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                if (connectionString == null)
                {
                    throw new ArgumentException("Empty database string connection");
                }
                
                options.UseSqlServer(connectionString);
            });


            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            services.AddApiConfig(Configuration);

            services.AddSwaggerConfig();

            services.ResolveDependencies();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseApiConfig(env);

            app.UseSwaggerConfig(provider);

            //Apply migrations
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<TodoListDbContext>();
            context.Database.Migrate();
        }
    }
}
