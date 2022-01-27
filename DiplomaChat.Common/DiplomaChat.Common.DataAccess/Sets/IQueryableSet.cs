using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Queryables;

namespace DiplomaChat.Common.DataAccess.Sets
{
    public interface IQueryableSet<TEntity> : IQueryable<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        void AddRange(IEnumerable<TEntity> entities);
        void AddRange(params TEntity[] entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task AddRangeAsync(params TEntity[] entities);

        TEntity Find(params object[] keyValues);
        ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);
        ValueTask<TEntity> FindAsync(params object[] keyValues);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void RemoveRange(params TEntity[] entities);

        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void UpdateRange(params TEntity[] entities);

        public IJoinableQueryable<TEntity, TProperty> JoinSet<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath);
    }
}
