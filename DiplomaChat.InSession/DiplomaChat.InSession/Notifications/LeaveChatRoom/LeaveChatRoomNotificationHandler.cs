using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.InSession.DataAccess.Contracts.Context;
using DiplomaChat.InSession.Domain.Entities;

namespace DiplomaChat.InSession.Notifications.LeaveGameSession
{
    public class LeaveChatRoomNotificationHandler : IRequestHandler<LeaveChatRoomNotificationCommand>
    {
        private readonly IInSessionContext _inSessionContext;

        public LeaveChatRoomNotificationHandler(IInSessionContext inSessionContext)
        {
            _inSessionContext = inSessionContext;
        }

        public Task<Unit> Handle(LeaveChatRoomNotificationCommand request, CancellationToken cancellationToken)
        {
            var chatRooms = _inSessionContext.EntitySet<ChatRoom>();
            var chatRoomWithUser = chatRooms.FirstOrDefault(s => s.ChatMembers.Any(p => p.Id == request.UserId));

            if (chatRoomWithUser != null)
            {
                var sessionPlayer = chatRoomWithUser.ChatMembers.Single(p => p.Id == request.UserId);

                chatRoomWithUser.ChatMembers.Remove(sessionPlayer);

                if (!chatRoomWithUser.ChatMembers.Any())
                {
                    chatRooms.Remove(chatRoomWithUser);
                }
            }

            return Unit.Task;
        }
    }
}