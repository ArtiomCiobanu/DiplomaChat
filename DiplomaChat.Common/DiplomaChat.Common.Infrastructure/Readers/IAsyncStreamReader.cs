using System.IO;
using System.Threading.Tasks;

namespace DiplomaChat.Common.Infrastructure.Readers
{
    public interface IAsyncStreamReader
    {
        Task<string> ReadStreamToTheEndAsync(Stream stream);
    }
}
