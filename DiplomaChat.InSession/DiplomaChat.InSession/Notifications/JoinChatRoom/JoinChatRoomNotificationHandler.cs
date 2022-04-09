using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.Domain.Entities;

namespace TileGameServer.InSession.Notifications.JoinChatRoom
{
    public class JoinChatRoomNotificationHandler : IRequestHandler<JoinChatRoomNotificationCommand>
    {
        private readonly IInSessionContext _inSessionContext;

        public JoinChatRoomNotificationHandler(IInSessionContext inSessionContext)
        {
            _inSessionContext = inSessionContext;
        }

        public Task<Unit> Handle(JoinChatRoomNotificationCommand request, CancellationToken cancellationToken)
        {
            var chatRoom = _inSessionContext.EntitySet<ChatRoom>()
                .FirstOrDefault(s => s.Id == request.ChatRoomId);

            if (chatRoom != null && chatRoom.ChatMembers.All(p => p.Id != request.UserId))
            {
                var chatMember = new ChatMember
                {
                    Id = request.UserId,
                    Nickname = request.UserNickname,

                    ChatRoom = chatRoom
                };

                chatRoom.ChatMembers.Add(chatMember);

                var players = _inSessionContext.EntitySet<ChatMember>();
                if (players.All(s => s.Id != request.UserId))
                {
                    players.Add(chatMember);
                }
            }

            return Unit.Task;
        }
    }
}