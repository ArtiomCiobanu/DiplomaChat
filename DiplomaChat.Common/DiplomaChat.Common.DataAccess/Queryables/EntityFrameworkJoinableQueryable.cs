using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DiplomaChat.Common.DataAccess.Queryables
{
    public class EntityFrameworkJoinableQueryable<TEntity, TProperty> : IJoinableQueryable<TEntity, TProperty>
        where TEntity : class
    {
        private readonly IIncludableQueryable<TEntity, TProperty> _includableQueryable;

        public EntityFrameworkJoinableQueryable(IIncludableQueryable<TEntity, TProperty> includableQueryable)
        {
            _includableQueryable = includableQueryable;
        }

        public Type ElementType => _includableQueryable.ElementType;
        public Expression Expression => _includableQueryable.Expression;
        public IQueryProvider Provider => _includableQueryable.Provider;

        public IEnumerator<TEntity> GetEnumerator() => _includableQueryable.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _includableQueryable.GetEnumerator();

        public IJoinableQueryable<TEntity, TNavigationProperty> JoinSet<TNavigationProperty>(Expression<Func<TEntity, TNavigationProperty>> navigationPropertyPath)
        {
            var includableQueryable = _includableQueryable.AsQueryable().Include(navigationPropertyPath);
            var joinableQueryable = new EntityFrameworkJoinableQueryable<TEntity, TNavigationProperty>(includableQueryable);

            return joinableQueryable;
        }
    }
}
