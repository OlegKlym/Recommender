using System;
using System.Reflection;
using NLog;
using Recommender.Core.Services;

namespace Recommender.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;

        public LoggingService()
        {
            _logger = LogManager
                .Setup()
                .LoadConfigurationFromAssemblyResource(typeof(App)
                .GetTypeInfo().Assembly)
                .GetCurrentClassLogger();
        }

        public void Error(string message) => _logger.Error(message);

        public void Error(Exception e, string message) => _logger.Error(e, message);

        public void Error(string format, params object[] args) => _logger.Error(format, args);

        public void Error(Exception e, string format, params object[] args) => _logger.Error(e, format, args);

        public void Info(string message) => _logger.Info(message);

        public void Info(Exception e, string message) => _logger.Info(e, message);

        public void Info(string format, params object[] args) => _logger.Info(format, args);

        public void Info(Exception e, string format, params object[] args) => _logger.Info(e, format, args);
    }
}
