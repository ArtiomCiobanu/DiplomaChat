using System.IO;
using System.Threading.Tasks;

namespace DiplomaChat.Common.Infrastructure.Readers
{
    public class AsyncStreamReader : IAsyncStreamReader
    {
        public async Task<string> ReadStreamToTheEndAsync(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var result = await new StreamReader(stream).ReadToEndAsync();
            stream.Seek(0, SeekOrigin.Begin);

            return result;
        }
    }
}
