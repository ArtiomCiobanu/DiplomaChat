using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiBaseLibrary.Controllers;

namespace TileGameServer.InSession.Controllers
{
    [Route("sessions")]
    [ApiController]
    public class SessionController : BaseMediatorController
    {
        public SessionController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize]
        [HttpGet("{sessionId}/members")]
        public Task<IActionResult> GetTileField(Guid sessionId)
        {
            return null;
        }
    }
}