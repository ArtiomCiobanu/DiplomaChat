using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Extensions;
using Microsoft.AspNetCore.Http;

namespace DiplomaChat.Common.Infrastructure.Logging.Accessors.Endpoint
{
    public class EndpointInformationAccessor : IEndpointInformationAccessor
    {
        private readonly HttpContext _httpContext;

        public EndpointInformationAccessor(IHttpContextAccessor accessor)
        {
            _httpContext = accessor.HttpContext;
        }

        public Guid? UserId => _httpContext.User.Claims.GetClaimValueIfExists<Guid>(WebApiClaimTypes.AccountId);
        public string Method => _httpContext.Request.Method;
        public string Path => _httpContext.Request.Path;
        public int StatusCode => _httpContext.Response.StatusCode;
    }
}
