using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Queryables;
using Microsoft.EntityFrameworkCore;

namespace DiplomaChat.Common.DataAccess.Sets
{
    public class EntityFrameworkQueryableSet<TEntity> : IQueryableSet<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> _entityDbSet;
        private readonly IQueryable<TEntity> _entityDbSetQueryable;

        public EntityFrameworkQueryableSet(DbSet<TEntity> entities)
        {
            _entityDbSet = entities;
            _entityDbSetQueryable = entities.AsQueryable();
        }

        Type IQueryable.ElementType => typeof(TEntity);
        Expression IQueryable.Expression => _entityDbSetQueryable.Expression;
        IQueryProvider IQueryable.Provider => _entityDbSetQueryable.Provider;

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => _entityDbSetQueryable.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _entityDbSetQueryable.GetEnumerator();

        void IQueryableSet<TEntity>.Add(TEntity entity) => _entityDbSet.Add(entity);
        Task IQueryableSet<TEntity>.AddAsync(TEntity entity, CancellationToken cancellationToken)
            => _entityDbSet.AddAsync(entity, cancellationToken).AsTask();

        void IQueryableSet<TEntity>.AddRange(IEnumerable<TEntity> entities) => _entityDbSet.AddRange(entities);
        void IQueryableSet<TEntity>.AddRange(params TEntity[] entities) => _entityDbSet.AddRange(entities);
        Task IQueryableSet<TEntity>.AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
            => _entityDbSet.AddRangeAsync(entities, cancellationToken);
        Task IQueryableSet<TEntity>.AddRangeAsync(params TEntity[] entities) => _entityDbSet.AddRangeAsync(entities);

        TEntity IQueryableSet<TEntity>.Find(params object[] keyValues) => _entityDbSet.Find(keyValues);
        ValueTask<TEntity> IQueryableSet<TEntity>.FindAsync(object[] keyValues, CancellationToken cancellationToken) => _entityDbSet.FindAsync(keyValues, cancellationToken);
        ValueTask<TEntity> IQueryableSet<TEntity>.FindAsync(params object[] keyValues) => _entityDbSet.FindAsync(keyValues);

        void IQueryableSet<TEntity>.Remove(TEntity entity) => _entityDbSet.Remove(entity);
        void IQueryableSet<TEntity>.RemoveRange(IEnumerable<TEntity> entities) => _entityDbSet.RemoveRange(entities);
        void IQueryableSet<TEntity>.RemoveRange(params TEntity[] entities) => _entityDbSet.RemoveRange(entities);

        void IQueryableSet<TEntity>.Update(TEntity entity) => _entityDbSet.Update(entity);
        void IQueryableSet<TEntity>.UpdateRange(IEnumerable<TEntity> entities) => _entityDbSet.UpdateRange(entities);
        void IQueryableSet<TEntity>.UpdateRange(params TEntity[] entities) => _entityDbSet.UpdateRange(entities);

        public IJoinableQueryable<TEntity, TProperty> JoinSet<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            var includableQueryable = _entityDbSet.Include(navigationPropertyPath);
            var joinableQueryable = new EntityFrameworkJoinableQueryable<TEntity, TProperty>(includableQueryable);

            return joinableQueryable;
        }
    }
}
