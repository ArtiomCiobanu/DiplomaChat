using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Sets;
using Microsoft.EntityFrameworkCore;

namespace DiplomaChat.Common.DataAccess.Context
{
    public abstract class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly Assembly _assembly;

        protected DatabaseContext(DbContextOptions options, Assembly assembly = null)
            : base(options)
        {
            _assembly = assembly;
        }

        IQueryableSet<TEntity> IDatabaseContext.EntitySet<TEntity>() where TEntity : class
            => new EntityFrameworkQueryableSet<TEntity>(Set<TEntity>());

        int IDatabaseContext.SaveChanges() => SaveChanges();

        int IDatabaseContext.SaveChanges(bool acceptAllChangesOnSuccess) => SaveChanges(acceptAllChangesOnSuccess);

        Task<int> IDatabaseContext.SaveChangesAsync(CancellationToken cancellationToken)
            => SaveChangesAsync(cancellationToken);

        Task<int> IDatabaseContext.SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
            => SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(_assembly);
        }

        /*public void BeginTransaction()
        {
            Database.BeginTransaction();
        }*/
    }
}