using DiplomaChat.Common.DataAccess.Entities;

namespace DiplomaChat.InSession.DataAccess.Contracts.Context
{
    public interface IInSessionContext
    {
        ICollection<TEntity> EntitySet<TEntity>() where TEntity : BaseEntity;
    }
}