using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure;
using DiplomaChat.Common.Infrastructure.Extensions;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.DataAccess.Contracts.Context;
using DiplomaChat.DataAccess.Entities;
using MediatR;

namespace DiplomaChat.Features.Rooms.LeaveAllChatRooms
{
    public record class LeaveAllChatRoomsCommand(Guid UserId) : IQuery<LeaveAllChatRoomsResponse>;

    public record class LeaveAllChatRoomsResponse(Guid[] RoomIds);

    public class LeaveAllChatRoomsHandler : IRequestHandler<LeaveAllChatRoomsCommand, IResponse<LeaveAllChatRoomsResponse>>
    {
        private readonly IDiplomaChatContext _diplomaChatContext;

        public LeaveAllChatRoomsHandler(IDiplomaChatContext diplomaChatContext)
        {
            _diplomaChatContext = diplomaChatContext;
        }

        public async Task<IResponse<LeaveAllChatRoomsResponse>> Handle(
            LeaveAllChatRoomsCommand request,
            CancellationToken cancellationToken)
        {
            var chatRoomUsersToRemove = _diplomaChatContext.EntitySet<ChatRoomUser>()
                .Where(cru => cru.UserId == request.UserId);

            var roomIds = await chatRoomUsersToRemove
                .Select(cru => cru.ChatRoomId)
                .GetArrayAsync(cancellationToken);

            _diplomaChatContext.EntitySet<ChatRoomUser>()
                .RemoveRange(chatRoomUsersToRemove);

            await _diplomaChatContext.SaveChangesAsync(cancellationToken);

            var response = new LeaveAllChatRoomsResponse(roomIds);
            return response.Success();
        }
    }
}
