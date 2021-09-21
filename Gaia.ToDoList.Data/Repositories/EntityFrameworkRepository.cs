using System.Linq;
using System.Threading.Tasks;
using Gaia.ToDoList.Business.Interfaces.DbContext;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaia.ToDoList.Data.Repositories
{
    public abstract class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly IDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected EntityFrameworkRepository(IDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task Add(TEntity entity) => await DbSet.AddAsync(entity);

        public virtual async Task Update(TEntity entity)
        {
            await Task.Run(() =>
            {
                var localEntity = DbSet.Local.FirstOrDefault(x => x.Id.Equals(entity.Id));

                var entry = DbContext.Entry(localEntity ?? entity);
                if (localEntity == null) DbSet.Attach(entity);
                else entry.CurrentValues.SetValues(entity);

                entry.State = EntityState.Modified;
            });
        }

        public virtual async Task Delete(long id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null) return;

            await Task.Run(() => DbSet.Remove(entity));
        }

        public virtual async Task<TEntity> GetById(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Commit()
        {
            await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(in bool disposing)
        {
            if (!disposing) return;

            DbContext?.Dispose();
        }
    }
}
