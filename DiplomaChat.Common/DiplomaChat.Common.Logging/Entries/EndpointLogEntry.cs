using System;

namespace DiplomaChat.Common.Logging.Entries
{
    public class EndpointLogEntry
    {
        public string Method { get; init; }
        public string RequestBody { get; init; }
        public string ResponseBody { get; init; }
        public string Path { get; init; }

        public int StatusCode { get; init; }

        public int Elapsed { get; init; }

        public Guid? AccountId { get; init; }
    }
}
