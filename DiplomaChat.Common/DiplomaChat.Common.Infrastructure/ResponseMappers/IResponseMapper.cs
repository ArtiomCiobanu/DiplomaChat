using System.Threading.Tasks;
using DiplomaChat.Common.Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaChat.Common.Infrastructure.ResponseMappers
{
    public interface IResponseMapper
    {
        IActionResult ExecuteAndMapStatus<TResult>(IResponse<TResult> response);

        Task<IActionResult> ExecuteAndMapStatusAsync<TResult>(IResponse<TResult> response);
        Task<IActionResult> ExecuteAndMapStatusAsync<TResult>(Task<IResponse<TResult>> responseTask);

        object GetResponseWithoutMessage<TResult>(IResponse<TResult> response);
        object GetMessageResponse<TResult>(IResponse<TResult> response);
    }
}