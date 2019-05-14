using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// <para>The ILoggerManager interface.</para>
    /// <para>It contains methods for the logger actions.</para>
    /// </summary>
    public interface ILoggerManager             //interface having logger actions
    {

        /// <summary>Logs the information.</summary>
        /// <param name="message">The message.</param>
        void LogInfo(string message);           //for logging any information.


        /// <summary>Logs the warn.</summary>
        /// <param name="message">The message.</param>
        void LogWarn(string message);


        /// <summary>Logs the debug.</summary>
        /// <param name="message">The message.</param>
        void LogDebug(string message);


        /// <summary>Logs the error.</summary>
        /// <param name="message">The message.</param>
        void LogError(string message);          //for logging errors.
    }
}
