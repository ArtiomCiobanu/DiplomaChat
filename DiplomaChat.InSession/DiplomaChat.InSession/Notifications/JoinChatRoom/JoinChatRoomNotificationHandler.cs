using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
            var sessions = _inSessionContext.EntitySet<ChatRoom>();
            var session = sessions.FirstOrDefault(s => s.Id == request.ChatRoomId);

            if (session != null && sessions.All(s => s.ChatMembers.All(p => p.Id != request.UserId)))
            {
                var chatMember = new ChatMember
                {
                    Id = request.UserId,
                    Nickname = request.PlayerNickname,

                    ChatRoom = session
                };

                session.ChatMembers.Add(chatMember);

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