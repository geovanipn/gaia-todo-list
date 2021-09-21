using System.Threading.Tasks;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Interfaces.Services;
using Gaia.ToDoList.Business.Models;

namespace Gaia.ToDoList.Business.Services
{
    public sealed class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository listRepository)
        {
            _itemRepository = listRepository;
        }

        public async Task<Item> Create(Item item)
        {
            await _itemRepository.Add(item);
            await _itemRepository.Commit();

            return item;
        }

        public async Task<Item> Delete(Item item)
        {
            await _itemRepository.Delete(item.Id);
            await _itemRepository.Commit();

            return item;
        }
    }
}
