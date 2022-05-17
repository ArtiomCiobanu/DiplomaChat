namespace DiplomaChat.Common.Infrastructure.Logging.Loggers;

public interface ILevelLogger
{
    void Information(string message = "");
    void Warning(string message = "");
    void Error(string message = "");
    void Fatal(string message = "");
}