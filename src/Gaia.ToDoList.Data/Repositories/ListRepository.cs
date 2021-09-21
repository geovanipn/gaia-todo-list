using System.Threading.Tasks;
using Gaia.ToDoList.Business.Interfaces.DbContext;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaia.ToDoList.Data.Repositories
{
    public class ListRepository : EntityFrameworkRepository<List>, IListRepository
    {
        public ListRepository(IDbContext context) : base(context)
        { }

        public override async Task<List> GetById(long id)
        {
            return await DbSet
                .Include(list => list.Items)
                .FirstAsync(list => list.Id == id);
        }
    }
}
