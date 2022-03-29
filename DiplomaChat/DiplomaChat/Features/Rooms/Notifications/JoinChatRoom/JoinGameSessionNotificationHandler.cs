using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.MessageQueueing.MessageQueueing;
using DiplomaChat.DataAccess.Context;
using DiplomaChat.DataAccess.Entities;
using MediatR;

namespace DiplomaChat.Features.Rooms.Notifications.JoinChatRoom
{
    public class JoinGameSessionNotificationHandler : IRequestHandler<JoinChatRoomNotificationCommand>
    {
        private readonly IMessageQueuePublisher _messageQueuePublisher;
        private readonly IDiplomaChatContext _diplomaChatContext;

        public JoinGameSessionNotificationHandler(
            IMessageQueueConnection messageQueueConnection,
            IDiplomaChatContext diplomaChatContext)
        {
            _messageQueuePublisher = messageQueueConnection.CreatePublisher("JoinChatRoomQueue");
            _diplomaChatContext = diplomaChatContext;
        }

        public async Task<Unit> Handle(JoinChatRoomNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                var user = await _diplomaChatContext.EntitySet<ChatRoomUser>()
                    .JoinSet(cru => cru.User)
                    .Where(u => u.Id == request.UserId)
                    .Select(u => new
                    {
                        u.Id,
                        ChatRoomId = u.ChatRoomId,
                        u.User.Nickname
                    })
                    .TopOneAsync(cancellationToken);

                if (user != null)
                {
                    _messageQueuePublisher.PublishMessage(
                        new JoinGameSessionNotification
                        {
                            UserId = request.UserId,
                            UserNickname = user.Nickname,
                            GameSessionId = request.ChatRoomId
                        });
                    _messageQueuePublisher.Dispose();
                }
            }

            return Unit.Value;
        }
    }

    public class JoinGameSessionNotification
    {
        public Guid UserId { get; set; }
        public Guid GameSessionId { get; set; }
        public string UserNickname { get; set; }
    }
}