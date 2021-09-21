using System.Threading.Tasks;
using Gaia.ToDoList.Business.Models;

namespace Gaia.ToDoList.Business.Interfaces.Services
{
    public interface IItemService
    {
        Task<Item> Create(Item item);

        Task<Item> Delete(Item item);
    }
}
