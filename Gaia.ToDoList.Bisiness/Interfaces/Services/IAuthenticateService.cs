using System.Threading.Tasks;
using Gaia.ToDoList.Business.OutputModels;

namespace Gaia.ToDoList.Business.Interfaces.Services
{
    public interface IAuthenticateService
    {
       Task<AuthorizationOutputModel> Authenticate(string login, string password);
    }
}
