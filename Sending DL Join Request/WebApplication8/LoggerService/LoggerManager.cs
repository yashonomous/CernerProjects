using System;
using NLog;
using Contracts;

namespace LoggerService
{
    /// <summary>Contains definition for all logger action methods.</summary>
    public class LoggerManager : ILoggerManager             //class defining logger actions
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>Initializes a new instance of the <see cref="LoggerManager"/> class.</summary>
        public LoggerManager()
        {
        }

        /// <summary>Logs the debug.</summary>
        /// <param name="message">The message.</param>
        public void LogDebug(string message)
        {
            logger.Debug(message);                  //logging the debug message.
        }

        /// <summary>Logs the error.</summary>
        /// <param name="message">The message.</param>
        public void LogError(string message)
        {
            logger.Error(message);                  //logging the error.
        }

        /// <summary>Logs the information.</summary>
        /// <param name="message">The message.</param>
        public void LogInfo(string message)
        {
            logger.Info(message);                   //logging the action info.
        }

        /// <summary>Logs the warn.</summary>
        /// <param name="message">The message.</param>
        public void LogWarn(string message)
        {
            logger.Warn(message);                   //logging the warnings.
        }
    }
}
