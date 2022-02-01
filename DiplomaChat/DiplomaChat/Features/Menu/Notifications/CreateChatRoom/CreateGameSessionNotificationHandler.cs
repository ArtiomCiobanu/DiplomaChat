using System;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.MessageQueueing.MessageQueueing;
using MediatR;

namespace TileGameServer.Features.Menu.Notifications.CreateChatRoom
{
    public class CreateGameSessionNotificationHandler : IRequestHandler<CreateChatRoomNotificationCommand>
    {
        private readonly IMessageQueuePublisher _messageQueuePublisher;

        public CreateGameSessionNotificationHandler(IMessageQueueConnection messageQueueConnection)
        {
            _messageQueuePublisher = messageQueueConnection.CreatePublisher("CreateChatRoomQueue");
        }

        public Task<Unit> Handle(CreateChatRoomNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                _messageQueuePublisher.PublishMessage(
                    new CreateGameSessionNotification
                    {
                        GameSessionId = request.GameSessionId
                    });

                _messageQueuePublisher.Dispose();
            }

            return Unit.Task;
        }
    }

    public class CreateGameSessionNotification
    {
        public Guid GameSessionId { get; set; }
    }
}