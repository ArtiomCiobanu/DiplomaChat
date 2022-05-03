using DiplomaChat.Common.DataAccess.Entities;
using DiplomaChat.InSession.DataAccess.Contracts.Context;
using System.Collections;

namespace DiplomaChat.InSession.DataAccess.Context
{
    public class InMemoryInSessionContext : IInSessionContext
    {
        private readonly Dictionary<Type, ICollection> _entitySets = new();

        public ICollection<TEntity> EntitySet<TEntity>()
            where TEntity : BaseEntity
        {
            var type = typeof(TEntity);
            if (!_entitySets.ContainsKey(type))
            {
                ICollection<TEntity> collection = new List<TEntity>();
                _entitySets.Add(type, (ICollection)collection);
            }

            var entitySet = (ICollection<TEntity>)_entitySets[type];
            return entitySet;
        }
    }
}