namespace DiplomaChat.Common.Infrastructure.Logging.Loggers.EntryLoggers;

public interface IEntryLogger<TEntry>
{
    void LogEntry(TEntry endpointLogEntry);
}