using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.Infrastructure.Extensions;
using DiplomaChat.Common.Infrastructure.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TileGameServer.DataAccess.Context;
using TileGameServer.DataAccess.Entities;
using TileGameServer.Domain.Models.Configurations;

namespace TileGameServer.Features.Menu.ListCreatedChatRoom
{
    public class ListCreatedGameSessionsHandler :
        IRequestHandler<ListCreatedChatRoomsRequest, IResponse<ListCreatedChatRoomsResponse>>
    {
        private readonly RequestLimitConfiguration _requestLimitConfiguration;

        private readonly IDiplomaChatContext _diplomaChatContext;

        public ListCreatedGameSessionsHandler(
            IDiplomaChatContext diplomaChatContext,
            RequestLimitConfiguration configuration)
        {
            _diplomaChatContext = diplomaChatContext;
            _requestLimitConfiguration = configuration;
        }

        public async Task<IResponse<ListCreatedChatRoomsResponse>> Handle(
            ListCreatedChatRoomsRequest request,
            CancellationToken cancellationToken)
        {
            var minRequestLimit = _requestLimitConfiguration.MinRequestLimit;
            var maxRequestLimit = _requestLimitConfiguration.MaxRequestLimit;

            var limit = request.Limit <= minRequestLimit
                        || request.Limit > maxRequestLimit
                ? _requestLimitConfiguration.Default
                : request.Limit;

            var chatRooms = await _diplomaChatContext.EntitySet<ChatRoom>()
                .Include(cr => cr.RoomUsers)
                .OrderBy(cr => cr.CreationDate)
                .Skip(request.Offset)
                .Take(limit)
                .Select(cr => new
                {
                    cr.Id,
                    cr.CreatorId,
                    RoomUserAmount = cr.RoomUsers.Count
                })
                .ToArrayAsync(cancellationToken);

            var chatRoomList = chatRooms.Select(
                cr =>
                {
                    var user = _diplomaChatContext.EntitySet<User>()
                        .Where(u => u.Id == cr.CreatorId)
                        .Select(u => new { u.Nickname })
                        .First();

                    return new ListedGameSession
                    {
                        Id = cr.Id,
                        CreatorNickname = user.Nickname,
                        PlayerAmount = cr.RoomUserAmount
                    };
                });

            var response = new ListCreatedChatRoomsResponse
            {
                GameSessions = chatRoomList.ToArray()
            };

            return response.Success();
        }
    }
}