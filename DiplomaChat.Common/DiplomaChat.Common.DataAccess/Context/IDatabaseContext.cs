using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Sets;

namespace DiplomaChat.Common.DataAccess.Context
{
    public interface IDatabaseContext
    {
        //void BeginTransaction();

        IQueryableSet<TEntity> EntitySet<TEntity>() where TEntity : class;

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}