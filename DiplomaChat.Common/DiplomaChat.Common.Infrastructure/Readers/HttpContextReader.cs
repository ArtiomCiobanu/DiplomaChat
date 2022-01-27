using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DiplomaChat.Common.Infrastructure.Readers
{
    public class HttpContextReader : IHttpContextReader
    {
        private readonly IAsyncStreamReader _asyncStreamReader;

        public HttpContextReader(IAsyncStreamReader asyncStreamReader)
        {
            _asyncStreamReader = asyncStreamReader;
        }

        public async Task<string> GetRequestBodyAsync(HttpRequest request)
        {
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();
            }

            var requestBody = await _asyncStreamReader.ReadStreamToTheEndAsync(request.Body);
            return requestBody;
        }
    }
}
