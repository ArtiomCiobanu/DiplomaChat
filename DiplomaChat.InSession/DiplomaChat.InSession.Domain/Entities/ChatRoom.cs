using DiplomaChat.Common.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace TileGameServer.InSession.Domain.Entities
{
    public class ChatRoom : BaseEntity
    {
        private readonly Lazy<List<ChatMember>> _sessionPlayers = new();
        public IList<ChatMember> ChatMembers => _sessionPlayers.Value;
    }
}