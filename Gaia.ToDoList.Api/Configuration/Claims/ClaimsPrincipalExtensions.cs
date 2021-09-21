using System;
using System.Security.Claims;

namespace Gaia.ToDoList.Api.Configuration.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            var id = claim?.Value;
            
            return string.IsNullOrEmpty(id) 
                ? default 
                : long.Parse(id);
        }
    }
}
