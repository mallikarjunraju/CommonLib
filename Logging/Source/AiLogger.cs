using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Common.Logging.CategoryFilters;

namespace Common.Logging
{
    /// <summary>
    /// Logging components using Azure insights.
    /// </summary>
    /// <seealso cref="Common.Logging.ILogger" />
    public class AiLogger : ILogger
    {
        /// <summary>
        /// Instance of a telemetry client.
        /// </summary>
        private readonly TelemetryClient telemetryClient;

        /// <summary>
        /// The correlation provider.
        /// </summary>
        private readonly ICorrelationProvider correlationProvider;

        /// <summary>
        /// The category filters.
        /// </summary>
        private readonly IEnumerable<ICategoryFilter> categoryFilters;


        #region Log Error

        /// <summary>
        /// Initialises a new instance of the <see cref="AiLogger"/> class.
        /// </summary>
        /// <param name="correlationProvider">The correlation provider.</param>
        /// <param name="categoryFilters">The category filters.</param>
        public AiLogger(ICorrelationProvider correlationProvider)
        {
            this.telemetryClient = new TelemetryClient();
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
        /// <summary>
        /// Writes an error message using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        /// <param name="value">Severity value.</param>
        public void ErrorDetails(string message, SeverityValue value)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            this.telemetryClient.TrackTrace(message, SeverityLevel.Error, this.correlationProvider.GetCorrelations(HttpContext.Current));
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
            this.telemetryClient.TrackTrace(string.Format(format, inputs), SeverityLevel.Error, this.correlationProvider.GetCorrelations(HttpContext.Current));
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
            var telemetry = new ExceptionTelemetry(exception);
            telemetry.Properties.Add("Extended properties", string.Format(format, inputs));
            var result = telemetry.Properties.Concat(this.correlationProvider.GetCorrelations(HttpContext.Current));
            if (result != null)
            {
                this.telemetryClient.TrackException(telemetry);
            }
        }

        /// <summary>
        /// Writes the exception details.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="value">Severity value.</param>
        public void ErrorDetails(Exception exception, SeverityValue value)
        {
                   this.telemetryClient.TrackException(exception, this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

        #endregion

        #region Log Information

        /// <summary>
        /// This method can be used where just a text message has to be logged.
        /// </summary>
        /// <param name="message">This field holds the information to be logged.</param>
        /// <param name="value">Severity value.</param>
        public void Information(string message, SeverityValue value)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            this.telemetryClient.TrackTrace(message, SeverityLevel.Information, this.correlationProvider.GetCorrelations(HttpContext.Current));
        }

        /// <param name="value">Severity value.</param>
        /// <param name="format">A format string that contains zero or more format items,
        /// which correspond to objects in the arguments array.</param>
        /// <param name="inputs">An object array containing zero or more objects to format.</param>
        public void Information(SeverityValue value, string format, params object[] inputs)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            this.telemetryClient.TrackTrace(string.Format(format, inputs), SeverityLevel.Information, this.correlationProvider.GetCorrelations(HttpContext.Current));
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
            var telemetry = new TraceTelemetry(string.Format(format, inputs), SeverityLevel.Information);
            telemetry.Properties.Add("Exception", ExceptionUtilities.FormatException(exception, includeContext: true));
            var result = telemetry.Properties.Concat(this.correlationProvider.GetCorrelations(HttpContext.Current));
            if (result != null)
            {
                this.telemetryClient.TrackTrace(telemetry);
            }
        }

        #endregion

        #region Log API trace

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
            var telemetry = new TraceTelemetry("Trace component call", SeverityLevel.Verbose);
            telemetry.Properties.Add("component", componentName);
            telemetry.Properties.Add("method", method);
            telemetry.Properties.Add("timespan", timespan.ToString());

            if (!string.IsNullOrWhiteSpace(properties))
            {
                telemetry.Properties.Add("properties", properties);
            }

            var result = telemetry.Properties.Concat(this.correlationProvider.GetCorrelations(HttpContext.Current));
            if (result != null)
            {
                this.telemetryClient.TrackTrace(telemetry);
            }
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

        #region Log warnings

        /// <summary>
        /// Writes a warning using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        /// <param name="value">Severity value.</param>
        public void Warning(string message, SeverityValue value)
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(value));
            if (filter == null || !filter.Execute(value)) return;
            this.telemetryClient.TrackTrace(message, SeverityLevel.Warning, this.correlationProvider.GetCorrelations(HttpContext.Current));
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
            this.telemetryClient.TrackTrace(string.Format(format, inputs), SeverityLevel.Warning, this.correlationProvider.GetCorrelations(HttpContext.Current));
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
            var telemetry = new TraceTelemetry(string.Format(format, inputs), SeverityLevel.Warning);
            telemetry.Properties.Add("Exception", ExceptionUtilities.FormatException(exception, includeContext: true));
            var result = telemetry.Properties.Concat(this.correlationProvider.GetCorrelations(HttpContext.Current));
            if (result != null)
            {
                this.telemetryClient.TrackTrace(telemetry);
            }
        }

        /// <summary>
        /// Tacks the event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="metrics">The metrics.</param>
        public void TackEvent(string eventName, IDictionary<string, string> properties, IDictionary<string, double> metrics, SeverityValue value)
        {
            this.telemetryClient.TrackEvent(eventName, properties, metrics);
        }

        #endregion
    }
}
