using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.MessageQueueing.MessageQueueing;
using MediatR;

namespace DiplomaChat.Features.Rooms.Notifications.CreateChatRoom
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
                        ChatRoomId = request.ChatRoomId
                    });

                _messageQueuePublisher.Dispose();
            }

            return Unit.Task;
        }
    }

    public class CreateGameSessionNotification
    {
        public Guid ChatRoomId { get; set; }
    }
}