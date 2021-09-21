using System.Threading.Tasks;
using Gaia.ToDoList.Business.Models;

namespace Gaia.ToDoList.Business.Interfaces.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<Item[]> GetByListId(long listId);
    }
}
