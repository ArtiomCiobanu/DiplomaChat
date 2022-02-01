using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.MessageQueueing.MessageQueueing;
using MediatR;
using TileGameServer.DataAccess.Context;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.Features.Menu.Notifications.LeaveChatRoom
{
    public class LeaveGameSessionNotificationHandler : IRequestHandler<LeaveChatRoomNotificationCommand>
    {
        private readonly IMessageQueuePublisher _leaveChatRoomPublisher;
        private readonly IDiplomaChatContext _diplomaChatContext;

        public LeaveGameSessionNotificationHandler(
            IMessageQueueConnection messageQueueConnection,
            IDiplomaChatContext diplomaChatContext)
        {
            _leaveChatRoomPublisher = messageQueueConnection.CreatePublisher("LeaveChatRoomQueue");
            _diplomaChatContext = diplomaChatContext;
        }

        public async Task<Unit> Handle(LeaveChatRoomNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                var user = await _diplomaChatContext.EntitySet<User>()
                    .Where(u => u.Id == request.UserId)
                    .Select(u => new { u.Id })
                    .TopOneAsync(cancellationToken);

                if (user != null)
                {
                    var message = new LeaveGameSessionNotification
                    {
                        PlayerId = request.UserId
                    };

                    _leaveChatRoomPublisher.PublishMessage(message);
                    _leaveChatRoomPublisher.Dispose();
                }
            }

            return Unit.Value;
        }
    }

    public class LeaveGameSessionNotification
    {
        public Guid PlayerId { get; set; }
    }
}