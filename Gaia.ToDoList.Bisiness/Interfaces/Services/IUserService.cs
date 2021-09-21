using System.Threading.Tasks;
using Gaia.ToDoList.Business.Models;

namespace Gaia.ToDoList.Business.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> Create(User user);

        Task<User> Update(User user);
    }
}
