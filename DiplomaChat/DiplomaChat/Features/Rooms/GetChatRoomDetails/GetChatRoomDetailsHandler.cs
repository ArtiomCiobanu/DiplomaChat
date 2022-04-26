using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Extensions;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.DataAccess.Contracts.Context;
using DiplomaChat.DataAccess.Entities;
using MediatR;

namespace DiplomaChat.Features.Rooms.GetChatRoomDetails
{
    public class GetChatRoomDetailsHandler : IRequestHandler<GetChatRoomDetailsQuery, IResponse<GetChatRoomDetailsResponse>>
    {
        private readonly IDiplomaChatContext _diplomaChatContext;

        public GetChatRoomDetailsHandler(IDiplomaChatContext diplomaChatContext)
        {
            _diplomaChatContext = diplomaChatContext;
        }

        public async Task<IResponse<GetChatRoomDetailsResponse>> Handle(
            GetChatRoomDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var chatRoomCreator = await _diplomaChatContext.EntitySet<ChatRoom>()
                .Where(cr => cr.Id == request.ChatRoomId)
                .Select(cr => new { cr.CreatorId })
                .TopOneAsync(cancellationToken);

            if (chatRoomCreator == null)
            {
                return new Response<GetChatRoomDetailsResponse>
                {
                    Message = "No chat room was found with provided id.",
                    Status = ResponseStatus.Conflict
                };
            }

            var chatRoomCreatorNickname = await _diplomaChatContext.EntitySet<User>()
                .Where(u => u.Id == chatRoomCreator.CreatorId)
                .Select(u => u.Nickname)
                .TopOneAsync(cancellationToken);

            var result = new GetChatRoomDetailsResponse()
            {
                CreatorNickname = chatRoomCreatorNickname
            };

            return result.Success();
        }
    }
}
