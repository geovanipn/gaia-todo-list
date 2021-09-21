using System.Linq;
using System.Threading.Tasks;
using Gaia.ToDoList.Business.Interfaces.DbContext;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaia.ToDoList.Data.Repositories
{
    public sealed class ItemRepository : EntityFrameworkRepository<Item>, IItemRepository
    {
        public ItemRepository(IDbContext context) : base(context)
        { }

        public async Task<Item[]> GetByListId(long listId)
        {
            return await DbSet.AsNoTracking()
                .Where(item => item.ListId == listId)
                .ToArrayAsync();
        }
    }
}
