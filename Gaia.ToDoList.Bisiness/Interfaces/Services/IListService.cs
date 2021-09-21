using System.Threading.Tasks;
using Gaia.ToDoList.Business.Models;

namespace Gaia.ToDoList.Business.Interfaces.Services
{
    public interface IListService
    {
        Task<List> Create(List list);

        Task<List> Delete(List list);
    }
}
