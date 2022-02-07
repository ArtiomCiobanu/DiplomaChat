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

        object GetMessageResponse<TResult>(IResponse<TResult> response);
    }
}