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

        [MessageQueueAction("CreateGameQueue")]
        public void ReceiveJoinGameNotification(CreateChatRoomNotificationCommand command)
        {
            _mediator.Send(command);
        }

        [MessageQueueAction("JoinGameQueue")]
        public void ReceiveJoinGameNotification(JoinChatRoomNotificationCommand command)
        {
            _mediator.Send(command);
        }

        [MessageQueueAction("LeaveGameQueue")]
        public void ReceiveLeaveGameNotification(LeaveChatRoomNotificationCommand command)
        {
            _mediator.Send(command);
        }
    }
}