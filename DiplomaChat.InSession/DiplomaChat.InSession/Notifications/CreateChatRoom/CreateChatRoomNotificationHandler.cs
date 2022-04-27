using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.InSession.DataAccess.Contracts.Context;
using DiplomaChat.InSession.Domain.Entities;

namespace DiplomaChat.InSession.Notifications.CreateChatRoom
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