using DiplomaChat.Common.Infrastructure.Extensions;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.DataAccess.Contracts.Context;
using DiplomaChat.DataAccess.Entities;
using MediatR;

namespace DiplomaChat.Features.Rooms.CreateChatRoom
{
    public class CreateGameSessionCommandHandler : IRequestHandler<CreateChatRoomCommand, IResponse<CreateChatRoomResponse>>
    {
        private readonly IDiplomaChatContext _diplomaChatContext;

        public CreateGameSessionCommandHandler(
            IDiplomaChatContext diplomaChatContext)
        {
            _diplomaChatContext = diplomaChatContext;
        }

        public async Task<IResponse<CreateChatRoomResponse>> Handle(
            CreateChatRoomCommand request,
            CancellationToken cancellationToken)
        {
            var chatRoom = new ChatRoom
            {
                Id = Guid.NewGuid(),
                CreatorId = request.AccountId,
                CreationDate = DateTime.Now
            };

            await _diplomaChatContext.EntitySet<ChatRoom>()
                .AddAsync(chatRoom, cancellationToken);

            await _diplomaChatContext.SaveChangesAsync(cancellationToken);

            var createGameSessionResponse = new CreateChatRoomResponse
            {
                ChatRoomId = chatRoom.Id
            };

            return createGameSessionResponse.Success();
        }
    }
}