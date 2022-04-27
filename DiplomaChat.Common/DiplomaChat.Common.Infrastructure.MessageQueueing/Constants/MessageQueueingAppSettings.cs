using System.Diagnostics.CodeAnalysis;

namespace DiplomaChat.Common.Infrastructure.MessageQueueing.Constants
{
    public static class MessageQueueingAppSettings
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQConfiguration = "RabbitMQConfiguration";
    }
}