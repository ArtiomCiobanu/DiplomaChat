﻿using DiplomaChat.Common.DataAccess.Entities;
using System.Collections.Generic;

namespace TileGameServer.InSession.DataAccess.Context
{
    public interface IInSessionContext
    {
        ICollection<TEntity> EntitySet<TEntity>() where TEntity : BaseEntity;
    }
}