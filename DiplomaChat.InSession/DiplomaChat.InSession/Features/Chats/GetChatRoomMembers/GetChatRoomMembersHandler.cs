using DiplomaChat.Common.Infrastructure;
using DiplomaChat.Common.Infrastructure.Extensions;
using DiplomaChat.Common.Infrastructure.Responses;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TileGameServer.InSession.DataAccess.Contracts.Context;
using TileGameServer.InSession.Domain.Entities;

namespace TileGameServer.InSession.Features.Chats.GetChatRoomMembers
{
    public record class GetChatRoomMembersQuery(Guid ChatRoomId) : IQuery<GetChatRoomMembersResponse>;

    public record class GetChatRoomMembersResponse(GetChatRoomMemberMember[] Members);

    public class GetChatRoomMemberMember
    {
        public Guid UserId { get; init; }

        public string Nickname { get; init; }
    }

    public class GetChatRoomMembersHandler : IRequestHandler<GetChatRoomMembersQuery, IResponse<GetChatRoomMembersResponse>>
    {
        private readonly IInSessionContext _inSessionContext;

        public GetChatRoomMembersHandler(IInSessionContext inSessionContext)
        {
            _inSessionContext = inSessionContext;
        }

        public Task<IResponse<GetChatRoomMembersResponse>> Handle(
            GetChatRoomMembersQuery request,
            CancellationToken cancellationToken)
        {
            var chatMembers = _inSessionContext.EntitySet<ChatMember>()
                 .Where(cm => cm.ChatRoom.Id == request.ChatRoomId)
                 .Select(cm => new GetChatRoomMemberMember
                 {
                     UserId = cm.Id,
                     Nickname = cm.Nickname
                 });

            var response = new GetChatRoomMembersResponse(chatMembers.ToArray());

            return Task.FromResult(response.Success());
        }
    }
}
