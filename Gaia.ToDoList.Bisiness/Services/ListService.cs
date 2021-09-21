using System.Threading.Tasks;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Interfaces.Services;
using Gaia.ToDoList.Business.Models;

namespace Gaia.ToDoList.Business.Services
{
    public sealed class ListService : IListService
    {
        private readonly IListRepository _listRepository;

        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public async Task<List> Create(List list)
        {
            await _listRepository.Add(list);
            await _listRepository.Commit();
            
            return list;
        }

        public async Task<List> Delete(List list)
        {
            await _listRepository.Delete(list.Id);
            await _listRepository.Commit();

            return list;
        }
    }
}
