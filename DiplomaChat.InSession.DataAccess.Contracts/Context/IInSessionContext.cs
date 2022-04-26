using DiplomaChat.Common.DataAccess.Entities;

namespace TileGameServer.InSession.DataAccess.Contracts.Context
{
    public interface IInSessionContext
    {
        ICollection<TEntity> EntitySet<TEntity>() where TEntity : BaseEntity;
    }
}