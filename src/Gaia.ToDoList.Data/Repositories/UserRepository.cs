using System.Threading.Tasks;
using Gaia.ToDoList.Business.Interfaces.DbContext;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaia.ToDoList.Data.Repositories
{
    public sealed class UserRepository : EntityFrameworkRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext context) : base(context)
        { }

        public async Task<User> GetByLogin(string login)
        {
            return await DbSet.AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == login);
        }
    }
}
