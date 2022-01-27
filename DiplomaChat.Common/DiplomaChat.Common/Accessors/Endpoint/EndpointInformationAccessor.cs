using System;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Extensions;
using Microsoft.AspNetCore.Http;

namespace DiplomaChat.Common.Accessors.Endpoint
{
    public class EndpointInformationAccessor : IEndpointInformationAccessor
    {
        private readonly HttpContext _httpContext;

        public EndpointInformationAccessor(IHttpContextAccessor accessor)
        {
            _httpContext = accessor.HttpContext;
        }

        public Guid? AccountId => _httpContext.User.Claims.GetClaimValueIfExists<Guid>(WebApiClaimTypes.AccountId);
        public string Method => _httpContext.Request.Method;
        public string Path => _httpContext.Request.Path;
        public int StatusCode => _httpContext.Response.StatusCode;
    }
}