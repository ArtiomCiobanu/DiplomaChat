using DiplomaChat.Common.Infrastructure.Enums;

namespace DiplomaChat.Common.Infrastructure.Responses
{
    public record Response<T> : IResponse<T>
    {
        object IResponse.Result => Result;

        public T Result { get; init; }
        public ResponseStatus Status { get; init; }

        public string Message { get; init; }
    }
}
