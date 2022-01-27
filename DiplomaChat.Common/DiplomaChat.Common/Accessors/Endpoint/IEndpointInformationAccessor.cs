using System;

namespace DiplomaChat.Common.Accessors.Endpoint
{
    public interface IEndpointInformationAccessor
    {
        Guid? AccountId { get; }

        string Method { get; }
        string Path { get; }

        int StatusCode { get; }
    }
}
