using DiplomaChat.Common.Infrastructure.Controllers;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TileGameServer.InSession.Features.Chats.GetChatRoomMembers;

namespace TileGameServer.InSession.Controllers
{
    [Route("chatRooms")]
    [ApiController]
    public class ChatRoomController : BaseMediatorController
    {
        public ChatRoomController(IMediator mediator, IResponseMapper responseMapper)
            : base(mediator, responseMapper)
        {
        }

        [Authorize]
        [HttpGet("{roomId}/members")]
        public Task<IActionResult> GetChatRoomMembers(Guid roomId) => SendToMediatorAsync(new GetChatRoomMembersQuery(roomId));
    }
}