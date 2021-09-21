using System.Collections.Generic;
using System.Security.Claims;
using Gaia.ToDoList.Api.Configuration.Claims;
using Gaia.ToDoList.Business.Interfaces.AppUser;
using Microsoft.AspNetCore.Http;

namespace Gaia.ToDoList.Api.Configuration.AppUser
{
    public sealed class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthenticatedUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public long GetUserId()
        {
            return IsAuthenticated() 
                ? _accessor.HttpContext.User.GetUserId() 
                : default;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
