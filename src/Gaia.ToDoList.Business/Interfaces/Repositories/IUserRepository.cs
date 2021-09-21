using System.Threading.Tasks;
using Gaia.ToDoList.Business.Models;

namespace Gaia.ToDoList.Business.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByLogin(string login);
    }
}
