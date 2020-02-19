using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Common.Logging.CategoryFilters;

namespace Common.Logging
{
    /// <summary>
    /// Logging implementation using system diagnostics.
    /// </summary>
    /// <seealso cref="Common.Logging.ILogger" />
    public class Logger : ILogger
    {
        /// <summary>
        /// The correlation provider.
        /// </summary>
        private readonly ICorrelationProvider correlationProvider;

        /// <summary>
        /// The category filters.
        /// </summary>
        private readonly IEnumerable<ICategoryFilter> categoryFilters;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger" /> class.
        /// </summary>
        /// <param name="correlationProvider">The correlation provider.</param>
        public Logger(ICorrelationProvider correlationProvider)
        {
            this.correlationProvider = correlationProvider;
            this.categoryFilters = new List<ICategoryFilter>
                              {
                                  new SeverityValueAll(),
                                  new SeverityValueCritical(),
                                  new SeverityValueError(),
                                  new SeverityValueInformation(),
                                  new SeverityValueOff(),
                                  new SeverityValueVerbose(),
                                  new SeverityValueWarning()
                              };
        }

        #region Log Information

        /// <summary>
        /// The method can be used where just a text message has to be logged.
        /// </summary>
        /// <param name="message">This field holds the information to be logged.</param>
        /// <param name="value">Severity value.</param>
        public void Information(string message, SeverityValue value)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            var info = $"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} - {message}";
            Trace.TraceInformation(info, this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

        /// <summary>
        /// This method Writes an informational message
        /// using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items,
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        public void Information(SeverityValue value, string format, params object[] inputs)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            Trace.TraceInformation(format, inputs, this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

        /// <summary>
        /// This method can be used where an information has to be logged when an exception occurs.
        /// </summary>
        /// <param name="exception">Exception details to be logged.</param>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items,
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        public void Information(Exception exception, SeverityValue value, string format, params object[] inputs)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            var message = string.Format(format, inputs);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(message);
            stringBuilder.Append(
                $"Exception Details={ExceptionUtilities.FormatException(exception, true)}");
            Trace.TraceInformation(stringBuilder.ToString(), this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

        #endregion

        #region Log Warnings

        /// <summary>
        /// Writes a warning using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        /// <param name="value">Severity value.</param>
        public void Warning(string message, SeverityValue value)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            Trace.TraceWarning(message, this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

        /// <summary>
        /// Writes the warning message using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items,
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        public void Warning(SeverityValue value, string format, params object[] inputs)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            Trace.TraceWarning(format, inputs, this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

        /// <summary>
        /// Writes the warning message with the specified exception details, array of objects
        /// and formatting information.
        /// </summary>
        /// <param name="exception">Exception details to be logged.</param>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items,
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        public void Warning(Exception exception, SeverityValue value, string format, params object[] inputs)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            var message = string.Format(format, inputs);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(message);
            stringBuilder.Append(
                $"Exception Details={ExceptionUtilities.FormatException(exception, true)}");
            Trace.TraceWarning(stringBuilder.ToString(), this.correlationProvider.GetCorrelations(HttpContext.Current));
        }
        #endregion

        #region Log Error

        /// <summary>
        /// Writes an error message using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        /// <param name="value">Severity value.</param>
        public void ErrorDetails(string message, SeverityValue value)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            Trace.TraceError(message, this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

        /// <summary>
        /// Writes an error message using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items,
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        public void ErrorDetails(SeverityValue value, string format, params object[] inputs)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            Trace.TraceError(format, inputs, this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

        /// <summary>
        /// Writes the error message with the specified exception details, array of objects
        /// and formatting information.
        /// </summary>
        /// <param name="exception">Exception details to be logged.</param>
        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items,
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        public void ErrorDetails(Exception exception, SeverityValue value, string format, params object[] inputs)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            var message = string.Format(format, inputs);
            var exceptionFormatter = new StringBuilder();
            exceptionFormatter.Append(message);
            exceptionFormatter.Append(
                string.Format(
                    $"Exception Details={ExceptionUtilities.FormatException(exception, includeContext: true)}"
                ));
            Trace.TraceError(exceptionFormatter.ToString(), this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

        /// <summary>
        /// Writes the exception details.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="value">Severity value.</param>
        public void ErrorDetails(Exception exception, SeverityValue value)
        {
            var defaultFormat = "\r\n-----------\r\n{0}";
            this.ErrorDetails(exception, value, defaultFormat, string.Empty);
        }

        #endregion

        #region Tace API

        /// <summary>
        /// Logs the trace of an API transaction with the time span of any particular method call.
        /// </summary>
        /// <param name="componentName">API component name.</param>
        /// <param name="method">API method for which the trace to be logged.</param>
        /// <param name="timespan">Time span of the API call.</param>
        /// <param name="value">Severity value.</param>
        public void TraceApi(string componentName, string method, TimeSpan timespan, SeverityValue value)
        {
            this.TraceApi(componentName, method, timespan, string.Empty, value);
        }

        /// <summary>
        /// Logs the trace information with the specified API component name,
        /// method name, time span and the properties.
        /// </summary>
        /// <param name="componentName">API component name.</param>
        /// <param name="method">Method name.</param>
        /// <param name="timespan">Time span.</param>
        /// <param name="properties">Method properties.</param>
        /// <param name="value">Severity value.</param>
        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties, SeverityValue value)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            string message = string.Concat("component:", componentName, ";method:", method, ";timespan:", timespan.ToString(), ";properties:", properties);
            Trace.TraceInformation(message, this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

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
        public void TraceApi(string componentName, string method, TimeSpan timespan, SeverityValue value, string format, params object[] inputs)
        {
            this.TraceApi(componentName, method, timespan, string.Format(format, inputs), value);
        }

        #endregion

        /// <summary>
        /// Tacks the event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="metrics">The metrics.</param>
        /// <param name="value">Severity value.</param>
        public void TackEvent(string eventName, IDictionary<string, string> properties, IDictionary<string, double> metrics, SeverityValue value)
        {
            Trace.TraceInformation(eventName, properties, metrics);
        }
    }
}
