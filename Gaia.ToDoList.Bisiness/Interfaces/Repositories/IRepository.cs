using System;
using System.Threading.Tasks;

namespace Gaia.ToDoList.Business.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable
    {
        Task Add(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(long id);

        Task<TEntity> GetById(long id);

        Task Commit();
    }
}
