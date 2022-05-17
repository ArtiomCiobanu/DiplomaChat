using DiplomaChat.Common.Infrastructure.MessageQueueing.Attributes;
using DiplomaChat.InSession.Notifications.CreateChatRoom;
using DiplomaChat.InSession.Notifications.JoinChatRoom;
using DiplomaChat.InSession.Notifications.LeaveGameSession;
using MediatR;

namespace DiplomaChat.InSession.MessageQueueServices
{
    [MessageQueueService]
    public class ChatRoomMessageQueueService
    {
        private readonly IMediator _mediator;

        public ChatRoomMessageQueueService(IMediator mediator)
        {
            _mediator = mediator;
        }

        [MessageQueueAction("CreateChatRoomQueue")]
        public void ReceiveCreateChatRoomNotification(CreateChatRoomNotificationCommand command)
        {
            _mediator.Send(command);
        }

        [MessageQueueAction("JoinChatRoomQueue")]
        public void ReceiveJoinChatRoomNotification(JoinChatRoomNotificationCommand command)
        {
            _mediator.Send(command);
        }

        [MessageQueueAction("LeaveChatRoomQueue")]
        public void ReceiveLeaveChatRoomNotification(LeaveChatRoomNotificationCommand command)
        {
            _mediator.Send(command);
        }
    }
}