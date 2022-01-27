namespace DiplomaChat.Common.Logging.EntryLoggers
{
    public interface IEntryLogger<TEntry>
    {
        void LogEntry(TEntry endpointLogEntry);
    }
}
