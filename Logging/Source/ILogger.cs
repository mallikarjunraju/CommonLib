using System;
using System.Collections.Generic;

namespace Common.Logging
{
    /// <summary>
    /// Interface to log information, warning, errors and trace API.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// This method can be used where just a text message has to be logged.
        /// </summary>
        /// <param name="message">This field holds the information to be logged.</param>
        /// <param name="value">Severity value.</param>
        void Information(string message, SeverityValue value);

        /// <summary>
        /// This method Writes an informational message
        /// using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items, 
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        void Information(SeverityValue value, string format, params object[] inputs);

        /// <summary>
        /// This method can be used where an information has to be logged when an exception occurs.
        /// </summary>
        /// <param name="exception">Exception details to be logged.</param>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items, 
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        void Information(Exception exception, SeverityValue value, string format, params object[] inputs);

        /// <summary>
        /// Writes a warning using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        /// <param name="value">Severity value.</param>
        void Warning(string message, SeverityValue value);

        /// <summary>
        /// Writes the warning message using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items, 
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        void Warning(SeverityValue value, string format, params object[] inputs);

        /// <summary>
        /// Writes the warning message with the specified exception details, array of objects 
        /// and formatting information.
        /// </summary>
        /// <param name="exception">Exception details to be logged.</param>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items, 
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        void Warning(Exception exception, SeverityValue value, string format, params object[] inputs);

        /// <summary>
        /// Writes an error message using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        /// <param name="value">Severity value.</param>
        void ErrorDetails(string message, SeverityValue value);

        /// <summary>
        /// Writes an error message using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items, 
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        void ErrorDetails(SeverityValue value, string format, params object[] inputs);

        /// <summary>
        /// Writes the error message with the specified exception details, array of objects 
        /// and formatting information.
        /// </summary>
        /// <param name="exception">Exception details to be logged.</param>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items, 
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        void ErrorDetails(Exception exception, SeverityValue value, string format, params object[] inputs);

        /// <summary>
        /// Writes the exception details.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="value">Severity value.</param>
        void ErrorDetails(Exception exception, SeverityValue value);

        /// <summary>
        /// Logs the trace of an API transaction with the time span of any particular method call.
        /// </summary>
        /// <param name="componentName">API component name.</param>
        /// <param name="method">API method for which the trace to be logged.</param>
        /// <param name="timespan">Time span of the API call.</param>
        /// <param name="value">Severity value.</param>
        void TraceApi(string componentName, string method, TimeSpan timespan, SeverityValue value);

        /// <summary>
        /// Logs the trace information with the specified API component name,
        /// method name, time span and the properties.
        /// </summary>
        /// <param name="componentName">API component name.</param>
        /// <param name="method">Method name.</param>
        /// <param name="timespan">Time span.</param>
        /// <param name="properties">Method properties.</param>
        /// <param name="value">Severity value.</param>
        void TraceApi(string componentName, string method, TimeSpan timespan, string properties, SeverityValue value);

        /// <summary>
        /// Logs the trace information with component name, method name, time span, array of objects 
        /// and formatting information.
        /// </summary>
        /// <param name="componentName">API component name.</param>
        /// <param name="method">Method name.</param>
        /// <param name="timespan">Time span.</param>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items, 
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        void TraceApi(string componentName, string method, TimeSpan timespan, SeverityValue value, string format, params object[] inputs);

        /// <summary>
        /// Tacks the event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="metrics">The metrics.</param>
        /// <param name="value">Severity value.</param>
        void TackEvent(string eventName, IDictionary<string, string> properties, IDictionary<string, double> metrics, SeverityValue value);
    }
}
