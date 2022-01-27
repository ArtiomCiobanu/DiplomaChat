using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.Common.Infrastructure.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaChat.Common.Infrastructure.Controllers
{
    public class BaseMediatorController : BaseController
    {
        protected IMediator Mediator { get; }

        public BaseMediatorController(IMediator mediator, IResponseMapper responseMapper)
            : base(responseMapper)
        {
            Mediator = mediator;
        }

        protected Task<IActionResult> SendToMediatorAsync<TResult>(
            IRequest<IResponse<TResult>> request,
            CancellationToken cancellationToken = default)
        {
            return ResponseMapper.ExecuteAndMapStatusAsync(Mediator.Send(request, cancellationToken));
        }
    }
}
