using Microsoft.Extensions.Logging;
using System;

namespace Learn.WebApp.Server.Orleans
{
    internal static class LearnClusterClientHostedServiceLoggerExtensions
    {
        private static readonly Action<ILogger, string, Exception?> _logServiceStarting = LoggerMessage.Define<string>(LogLevel.Information, new EventId(0, nameof(ServiceStarting)), "{ServiceName} starting...");
        private static readonly Action<ILogger, string, Exception?> _logServiceStarted = LoggerMessage.Define<string>(LogLevel.Information, new EventId(0, nameof(ServiceStarted)), "{ServiceName} started successfully");
        private static readonly Action<ILogger, string, Exception?> _logServiceStopped = LoggerMessage.Define<string>(LogLevel.Information, new EventId(0, nameof(ServiceStopped)), "{ServiceName} stopped successfully");
        private static readonly Action<ILogger, string, int, int, Exception?> _logFailedToConnectWillRetry = LoggerMessage.Define<string, int, int>(LogLevel.Warning, new EventId(0, nameof(FailedToConnectWillRetry)), "{ServiceName} failed to connect to the orleans cluster on attempt {Attempt} of {MaxAttempts} and will retry");
        private static readonly Action<ILogger, string, int, int, Exception?> _logFailedToConnectWillNotRetry = LoggerMessage.Define<string, int, int>(LogLevel.Error, new EventId(0, nameof(FailedToConnectWillNotRetry)), "{ServiceName} failed to connect to the orleans cluster on attempt {Attempt} of {MaxAttempts} and will not retry");

        public static void ServiceStarting(this ILogger logger, string serviceName)
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            _logServiceStarting(logger, serviceName, null);
        }

        public static void ServiceStarted(this ILogger logger, string serviceName)
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            _logServiceStarted(logger, serviceName, null);
        }

        public static void ServiceStopped(this ILogger logger, string serviceName)
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            _logServiceStopped(logger, serviceName, null);
        }

        public static void FailedToConnectWillRetry(this ILogger logger, string serviceName, int attempt, int maxAttempts)
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            _logFailedToConnectWillRetry(logger, serviceName, attempt, maxAttempts, null);
        }

        public static void FailedToConnectWillNotRetry(this ILogger logger, string serviceName, int attempt, int maxAttempts)
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            _logFailedToConnectWillNotRetry(logger, serviceName, attempt, maxAttempts, null);
        }
    }
}