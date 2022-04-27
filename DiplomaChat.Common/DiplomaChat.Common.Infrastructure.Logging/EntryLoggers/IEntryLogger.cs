namespace DiplomaChat.Common.Infrastructure.Logging.EntryLoggers
{
    public interface IEntryLogger<TEntry>
    {
        void LogEntry(TEntry endpointLogEntry);
    }
}
