using System;
using System.Threading.Tasks;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaChat.Common.Infrastructure.ResponseMappers
{
    public class ResponseMapper : IResponseMapper
    {
        public IActionResult ExecuteAndMapStatus<TResult>(IResponse<TResult> response)
        {
            object responseBody = string.IsNullOrEmpty(response.Message)
                ? response.Result
                : GetMessageResponse(response);

            int statusCode = GetStatusCode(response.Status);
            var actionResult = new ObjectResult(responseBody)
            {
                StatusCode = statusCode
            };

            return actionResult;
        }

        public Task<IActionResult> ExecuteAndMapStatusAsync<TResult>(IResponse<TResult> response)
        {
            return Task.FromResult(ExecuteAndMapStatus(response));
        }

        public async Task<IActionResult> ExecuteAndMapStatusAsync<TResult>(Task<IResponse<TResult>> responseTask)
        {
            var response = await responseTask;
            return ExecuteAndMapStatus(response);
        }

        public object GetMessageResponse<TResult>(IResponse<TResult> response)
        {
            return (response.Result is Unit || response.Result == null)
               ? new
               {
                   Message = response.Message
               }
               : new
               {
                   Value = response.Result,
                   Message = response.Message
               };
        }

        public int GetStatusCode(ResponseStatus responseStatus)
        {
            return responseStatus switch
            {
                ResponseStatus.InternalServerError => StatusCodes.Status500InternalServerError,
                ResponseStatus.Success => StatusCodes.Status200OK,
                ResponseStatus.BadRequest => StatusCodes.Status400BadRequest,
                ResponseStatus.Conflict => StatusCodes.Status409Conflict,
                ResponseStatus.NoContent => StatusCodes.Status204NoContent,
                ResponseStatus.NotFound => StatusCodes.Status404NotFound,
                ResponseStatus.Unauthorized => StatusCodes.Status401Unauthorized,
                ResponseStatus.Accepted => StatusCodes.Status202Accepted,
                ResponseStatus.PartialContent => StatusCodes.Status206PartialContent,
                ResponseStatus.Forbidden => StatusCodes.Status403Forbidden,
                ResponseStatus.Created => StatusCodes.Status201Created,
                ResponseStatus.TooManyRequests => StatusCodes.Status429TooManyRequests,
                _ => throw new ArgumentOutOfRangeException(
                    $"{responseStatus}",
                    "Should be a valid HTTP Status Code")
            };
        }
    }
}
