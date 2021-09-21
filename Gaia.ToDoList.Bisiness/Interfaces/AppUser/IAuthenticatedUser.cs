using System.Collections.Generic;
using System.Security.Claims;

namespace Gaia.ToDoList.Business.Interfaces.AppUser
{
    public interface IAuthenticatedUser
    {
        string Name { get; }
        
        long GetUserId();
        
        bool IsAuthenticated();
        
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
