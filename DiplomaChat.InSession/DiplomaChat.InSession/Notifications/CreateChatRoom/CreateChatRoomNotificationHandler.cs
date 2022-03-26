using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.Domain.Entities;

namespace TileGameServer.InSession.Notifications.CreateChatRoom
{
    public class CreateChatRoomNotificationHandler : IRequestHandler<CreateChatRoomNotificationCommand>
    {
        private readonly IInSessionContext _inSessionContext;

        public CreateChatRoomNotificationHandler(
            IInSessionContext inSessionContext)
        {
            _inSessionContext = inSessionContext;
        }

        public Task<Unit> Handle(CreateChatRoomNotificationCommand request, CancellationToken cancellationToken)
        {
            var chatRooms = _inSessionContext.EntitySet<ChatRoom>();
            if (chatRooms.Any(s => s.Id == request.ChatRoomId))
            {
                return Unit.Task;
            }

            var chatRoom = new ChatRoom()
            {
                Id = request.ChatRoomId,
            };

            chatRooms.Add(chatRoom);

            return Unit.Task;
        }
    }
}