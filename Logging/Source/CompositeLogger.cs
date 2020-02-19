using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    /// <summary>
    /// CompositeLogger to handle logging requests for Application insight & Loggly.
    /// </summary>
    /// <seealso cref="Common.Logging.ILogger" />
    public class CompositeLogger : ILogger
    {
        /// <summary>
        /// List of loggers.
        /// </summary>
        private readonly IEnumerable<ILogger> loggerList = null;

        /// <summary>
        /// Initialises a new instance of the <see cref="CompositeLogger"/> class.
        /// </summary>
        /// <param name="loggerList">The list of loggers.</param>
        public CompositeLogger(IEnumerable<ILogger> loggerList)
        {
            this.loggerList = loggerList;
        }

        /// <summary>
        /// This method will be used to call the Application insight and loggly operation with input parameters.
        /// </summary>
        /// <param name="message">This field holds the information to be logged.</param>
        /// <param name="value">Severity value.</param>
        public void Information(string message, SeverityValue value)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            { logger.Information(message, value); }
        }

        public void ErrorDetails(Exception exception, SeverityValue value)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            { logger.ErrorDetails(exception, value); }
        }

        public void ErrorDetails(string message, SeverityValue value)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            { logger.ErrorDetails(message, value); }
        }

        public void ErrorDetails(SeverityValue value, string format, params object[] inputs)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            { logger.ErrorDetails(value, format, inputs); }
        }

        public void ErrorDetails(Exception exception, SeverityValue value, string format, params object[] inputs)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            { logger.ErrorDetails(exception, value, format, inputs); }
        }

        

        public void Information(SeverityValue value, string format, params object[] inputs)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            { logger.Information(value, format, inputs); }
        }

        public void Information(Exception exception, SeverityValue value, string format, params object[] inputs)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            { logger.Information(exception, value, format, inputs); }
        }

        public void TackEvent(string eventName, IDictionary<string, string> properties, IDictionary<string, double> metrics, SeverityValue value)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            { logger.TackEvent(eventName, properties, metrics, value); }
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, SeverityValue value)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            { logger.TraceApi(componentName, method, timespan, value); }
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties, SeverityValue value)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            {
                logger.TraceApi(componentName, method, timespan, properties, value);
            }

        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, SeverityValue value, string format, params object[] inputs)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            {
                logger.TraceApi(componentName, method, timespan, value, format, inputs);
            }
        }

        public void Warning(string message, SeverityValue value)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            {
                logger.Warning(message, value);
            }
        }

        public void Warning(SeverityValue value, string format, params object[] inputs)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            {
                logger.Warning(value, format, inputs);
            }
        }

        public void Warning(Exception exception, SeverityValue value, string format, params object[] inputs)
        {
            foreach (ILogger logger in this.loggerList.ToList())
            {
                logger.Warning(exception, value, format, inputs);
            }
        }
    }
}
