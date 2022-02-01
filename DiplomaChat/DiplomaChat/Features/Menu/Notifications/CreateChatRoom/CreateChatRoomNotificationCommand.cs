using System;
using DiplomaChat.Common.Infrastructure;
using DiplomaChat.Common.Infrastructure.Enums;

namespace TileGameServer.Features.Menu.Notifications.CreateChatRoom
{
    public class CreateChatRoomNotificationCommand : ICommand
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid GameSessionId { get; set; }
    }
}