using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DiplomaChat.Common.Infrastructure.Readers
{
    public interface IHttpContextReader
    {
        Task<string> GetRequestBodyAsync(HttpRequest request);
    }
}
