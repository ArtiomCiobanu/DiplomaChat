using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DiplomaChat.Common.DataAccess.Extensions
{
    public static class QueryableExtensions
    {
        public static Task<bool> EveryAsync<TEntity>(
            this IQueryable<TEntity> queryable,
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return (queryable is IAsyncEnumerable<TEntity>)
                ? queryable.AllAsync(predicate, cancellationToken)
                : Task.FromResult(queryable.Any(predicate));
        }

        public static Task<bool> ExistsAsync<TEntity>(
            this IQueryable<TEntity> queryable,
            CancellationToken cancellationToken = default)
        {
            return (queryable is IAsyncEnumerable<TEntity>)
                ? queryable.AnyAsync(cancellationToken)
                : Task.FromResult(queryable.Any());
        }

        public static Task<bool> ExistsAsync<TEntity>(
            this IQueryable<TEntity> queryable,
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return (queryable is IAsyncEnumerable<TEntity>)
                ? queryable.AnyAsync(predicate, cancellationToken)
                : Task.FromResult(queryable.Any(predicate));
        }

        public static Task<TEntity> TopOneAsync<TEntity>(
            this IQueryable<TEntity> queryable,
            CancellationToken cancellationToken = default)
        {
            return (queryable is IAsyncEnumerable<TEntity>)
                ? queryable.FirstOrDefaultAsync(cancellationToken)
                : Task.FromResult(queryable.FirstOrDefault());
        }

        public static Task<TEntity> TopOneAsync<TEntity>(
            this IQueryable<TEntity> queryable,
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return (queryable is IAsyncEnumerable<TEntity>)
                ? queryable.FirstOrDefaultAsync(predicate, cancellationToken)
                : Task.FromResult(queryable.FirstOrDefault(predicate));
        }

        public static Task<List<TEntity>> GetListAsync<TEntity>(
            this IQueryable<TEntity> queryable,
            CancellationToken cancellationToken = default)
        {
            return (queryable is IAsyncEnumerable<TEntity>)
                ? queryable.ToListAsync(cancellationToken)
                : Task.FromResult(queryable.ToList());
        }

        public static Task<TEntity[]> GetArrayAsync<TEntity>(
            this IQueryable<TEntity> queryable,
            CancellationToken cancellationToken = default)
        {
            return (queryable is IAsyncEnumerable<TEntity>)
                ? queryable.ToArrayAsync(cancellationToken)
                : Task.FromResult(queryable.ToArray());
        }
    }
}