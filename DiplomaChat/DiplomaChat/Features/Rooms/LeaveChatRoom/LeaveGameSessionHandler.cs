using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.DataAccess.Context;
using DiplomaChat.DataAccess.Entities;
using MediatR;

namespace DiplomaChat.Features.Rooms.LeaveChatRoom
{
    public class LeaveGameSessionCommandHandler
        : IRequestHandler<LeaveChatRoomCommand, IResponse<Unit>>
    {
        private readonly IDiplomaChatContext _diplomaChatContext;

        public LeaveGameSessionCommandHandler(IDiplomaChatContext diplomaChatContext)
        {
            _diplomaChatContext = diplomaChatContext;
        }

        public async Task<IResponse<Unit>> Handle(
            LeaveChatRoomCommand request,
            CancellationToken cancellationToken)
        {
            var room = await _diplomaChatContext.EntitySet<ChatRoom>()
                .Where(u => u.Id == request.ChatRoomId)
                .Select(u => new { u.Id })
                .TopOneAsync(cancellationToken);

            if (room == null)
            {
                return new Response<Unit>
                {
                    Status = ResponseStatus.Conflict
                };
            }

            var roomUser = await _diplomaChatContext.EntitySet<ChatRoomUser>()
                .Where(u => u.ChatRoomId == room.Id)
                .Select(u => new { u.Id })
                .TopOneAsync(cancellationToken);

            if (roomUser == null)
            {
                return new Response<Unit>
                {
                    Status = ResponseStatus.Conflict
                };
            }

            var userToRemove = _diplomaChatContext.EntitySet<ChatRoomUser>()
                .Where(u => u.Id == roomUser.Id);
            _diplomaChatContext.EntitySet<ChatRoomUser>()
                .RemoveRange(userToRemove);

            await _diplomaChatContext.SaveChangesAsync(cancellationToken);

            var roomUsersExist = await _diplomaChatContext
                .EntitySet<ChatRoomUser>()
                .ExistsAsync(u => u.ChatRoomId == request.ChatRoomId, cancellationToken);

            if (!roomUsersExist)
            {
                var roomToRemove = _diplomaChatContext.EntitySet<ChatRoom>()
                    .Where(cr => cr.Id == request.ChatRoomId);
                _diplomaChatContext.EntitySet<ChatRoom>()
                    .RemoveRange(roomToRemove);
                await _diplomaChatContext.SaveChangesAsync(cancellationToken);
            }

            return new Response<Unit>
            {
                Status = ResponseStatus.Success
            };
        }
    }
}