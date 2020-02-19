using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Logging
{
    /// <summary>
    /// Provides correlation details for logging.
    /// </summary>
    /// <seealso cref="Common.Logging.ICorrelationProvider" />
    public class CorrelationProvider : ICorrelationProvider
    {
        /// <summary>
        /// Gets the correlations.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Header details that has to be logged.
        /// </returns>
        public IDictionary<string, string> GetCorrelations(HttpContext context)
        {
            if (context != null && context.Request != null && context.Request.Headers != null)
            {
                var correlationId = context.Request.Headers.GetValues("X-CorrelationId") != null
                    ? context.Request.Headers.GetValues("X-CorrelationId").FirstOrDefault() : null;

                return new Dictionary<string, string>
                {
                    { "Correlation Id", correlationId }
                };
            }

            return null;
        }
    }
}
