using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Features.Rooms.Notifications.LeaveChatRoom;
using MediatR;

namespace DiplomaChat.Features.Rooms.Notifications.LeaveAllChatRooms
{
    public class LeaveAllChatRoomsNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; init; }

        public Guid UserId { get; init; }

        public int RoomAmount { get; init; }
    }

    public class LeaveAllChatRoomsNotificationHandler : IRequestHandler<LeaveAllChatRoomsNotificationCommand, Unit>
    {
        private readonly IMediator _mediator;

        public LeaveAllChatRoomsNotificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Unit> Handle(
            LeaveAllChatRoomsNotificationCommand request,
            CancellationToken cancellationToken)
        {
            for (int i = 0; i < request.RoomAmount; i++)
            {
                var leaveAllChatRoomsNotificationCommand = new LeaveChatRoomNotificationCommand
                {
                    ResponseStatus = request.ResponseStatus,
                    UserId = request.UserId
                };

                _mediator.Send(leaveAllChatRoomsNotificationCommand);
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
