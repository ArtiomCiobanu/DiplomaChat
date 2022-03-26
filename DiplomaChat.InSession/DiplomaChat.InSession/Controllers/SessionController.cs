using DiplomaChat.Common.Infrastructure.Controllers;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TileGameServer.InSession.Controllers
{
    [Route("sessions")]
    [ApiController]
    public class SessionController : BaseMediatorController
    {
        public SessionController(IMediator mediator, IResponseMapper responseMapper)
            : base(mediator, responseMapper)
        {
        }


        [Authorize]
        [HttpGet("{roomId}/members")]
        public Task<IActionResult> GetChatRoomMembers(Guid roomId)
        {
            return null;
        }
    }
}