using DiplomaChat.Common.Infrastructure.Responses;
using MediatR;

namespace DiplomaChat.Common.Infrastructure
{
    public interface IQuery<out TResult> : IRequest<IResponse<TResult>>
    {
    }
}
