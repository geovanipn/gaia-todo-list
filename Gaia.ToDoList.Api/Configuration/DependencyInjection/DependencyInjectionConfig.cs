using Gaia.ToDoList.Api.Configuration.AppUser;
using Gaia.ToDoList.Api.Configuration.Swagger;
using Gaia.ToDoList.Business.Interfaces.AppUser;
using Gaia.ToDoList.Business.Interfaces.DbContext;
using Gaia.ToDoList.Business.Interfaces.Notifier;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Interfaces.Services;
using Gaia.ToDoList.Business.Notifier;
using Gaia.ToDoList.Business.Services;
using Gaia.ToDoList.Data.Context;
using Gaia.ToDoList.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Gaia.ToDoList.Api.Configuration.DependencyInjection
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDbContext, TodoListDbContext>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IListRepository, ListRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();

            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IListService, ListService>(); 
            services.AddScoped<IItemService, ItemService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            
            return services;
        }
    }
}
