using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DiplomaChat.Common.DataAccess.Queryables
{
    public interface IJoinableQueryable<TEntity, TProperty> : IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable
        where TEntity : class
    {
        IJoinableQueryable<TEntity, TNavigationProperty> JoinSet<TNavigationProperty>(Expression<Func<TEntity, TNavigationProperty>> navigationPropertyPath);
    }
}
