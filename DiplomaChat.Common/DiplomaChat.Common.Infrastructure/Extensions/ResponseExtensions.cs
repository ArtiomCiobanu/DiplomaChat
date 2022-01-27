using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Responses;

namespace DiplomaChat.Common.Infrastructure.Extensions
{
    public static class ResponseExtensions
    {
        public static IResponse<T> GetResponse<T>(this T result, ResponseStatus status, string message = null)
            => new Response<T>()
            {
                Result = result,
                Status = status,
                Message = message
            };

        public static IResponse<T> Success<T>(this T result, string message = null) 
            => GetResponse(result, ResponseStatus.Success, message);

        public static IResponse<T> Forbidden<T>(this T result, string message = null)
            => GetResponse(result, ResponseStatus.Forbidden, message);

        public static IResponse<T> Unauthorized<T>(this T result, string message = null)
            => GetResponse(result, ResponseStatus.Unauthorized, message);
    }
}
