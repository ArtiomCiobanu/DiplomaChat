using DiplomaChat.Common.MessageQueueing.Attributes;
using MediatR;
using TileGameServer.InSession.Notifications.CreateChatRoom;
using TileGameServer.InSession.Notifications.JoinChatRoom;
using TileGameServer.InSession.Notifications.LeaveGameSession;

namespace TileGameServer.InSession.MessageQueueServices
{
    [MessageQueueService]
    public class MenuMessageQueueService
    {
        private readonly IMediator _mediator;

        public MenuMessageQueueService(IMediator mediator)
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