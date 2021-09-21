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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //options.UseInMemoryDatabase("localDb");
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
        }
    }
}
