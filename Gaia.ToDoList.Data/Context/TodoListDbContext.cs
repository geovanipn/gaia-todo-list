using System.Linq;
using Gaia.ToDoList.Business.Interfaces.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Gaia.ToDoList.Data.Context
{
    public sealed class TodoListDbContext : DbContext, IDbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options) { }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class => base.Set<TEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoListDbContext).Assembly);

            RemoveConventions(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void RemoveConventions(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
