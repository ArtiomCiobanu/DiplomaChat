using DiplomaChat.Common.Infrastructure.Enums;

namespace DiplomaChat.Common.Infrastructure.Responses
{
    public interface IResponse<out TResult> : IResponse
    {
        new TResult Result { get; }
    }

    public interface IResponse
    {
        object Result { get; }
        ResponseStatus Status { get; }
        string Message { get; }
    }
}
