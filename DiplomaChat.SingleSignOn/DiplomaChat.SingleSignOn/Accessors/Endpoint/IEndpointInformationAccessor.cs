﻿using System;

namespace DiplomaChat.SingleSignOn.Accessors.Endpoint
{
    public interface IEndpointInformationAccessor
    {
        Guid? UserId { get; }

        string Method { get; }
        string Path { get; }

        int StatusCode { get; }
    }
}
