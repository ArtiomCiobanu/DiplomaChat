using System.Diagnostics.CodeAnalysis;

namespace DiplomaChat.Constants
{
    public static class EnvironmentVariables
    {
        public const string DatabaseConnectionString = "DIPLOMA_CHAT_CORE_DB_CONNECTION_STRING";
        
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQHostName = "DIPLOMA_CHAT_CORE_RABBITMQ_HOSTNAME";

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQPort = "DIPLOMA_CHAT_CORE_RABBITMQ_PORT";

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQVirtualHost = "DIPLOMA_CHAT_CORE_RABBITMQ_VIRTUALHOST";

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQUserName = "DIPLOMA_CHAT_CORE_RABBITMQ_USERNAME";

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQPassword = "DIPLOMA_CHAT_CORE_RABBITMQ_PASSWORD";
    }
}