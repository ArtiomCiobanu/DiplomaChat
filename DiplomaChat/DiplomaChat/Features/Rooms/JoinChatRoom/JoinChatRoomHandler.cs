using DiplomaChat.Common.Authorization.Generators;
using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.DataAccess.Context;
using DiplomaChat.DataAccess.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomaChat.Features.Rooms.JoinChatRoom
{
    public class JoinChatRoomHandler : IRequestHandler<JoinChatRoomCommand, IResponse<Unit>>
    {
        private readonly IDiplomaChatContext _diplomaChatContext;
        private readonly IJwtGenerator _jwtGenerator;

        public JoinChatRoomHandler(
            IDiplomaChatContext diplomaChatContext,
            IJwtGenerator jwtGenerator)
        {
            _diplomaChatContext = diplomaChatContext;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<IResponse<Unit>> Handle(
            JoinChatRoomCommand request,
            CancellationToken cancellationToken)
        {
            var room = await _diplomaChatContext.EntitySet<ChatRoom>()
                .Where(cr => cr.Id == request.RoomId)
                .Select(cr => new { cr.Id })
                .TopOneAsync(cancellationToken);

            if (room == null)
            {
                return new Response<Unit>
                {
                    Message = "No chat room with provided id was found",
                    Status = ResponseStatus.Conflict
                };
            }

            var chatRoomUser = new ChatRoomUser
            {
                Id = Guid.NewGuid(),
                UserId = request.AccountId,
                ChatRoomId = room.Id
            };

            await _diplomaChatContext.EntitySet<ChatRoomUser>()
                .AddAsync(chatRoomUser, cancellationToken);
            await _diplomaChatContext.SaveChangesAsync(cancellationToken);

            return new Response<Unit>
            {
                Status = ResponseStatus.Success
            };
        }
    }
}