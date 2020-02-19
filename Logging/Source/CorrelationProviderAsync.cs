using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Common.Logging
{
    /// <summary>
    /// Provides correlation details for logging.
    /// </summary>
    /// <seealso cref="Common.Logging.ICorrelationProvider" />
    public class CorrelationProviderAsync : ICorrelationProviderAsync
    {
        /// <summary>
        /// Gets the correlations.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Header details that has to be logged.
        /// </returns>
        public async Task<Dictionary<string, string>> GetCorrelationsAsync(HttpContext context)
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Headers != null)
            {
                var correlationId =  new Dictionary<string, string>
                {
                    {
                        "Correlation Id", HttpContext.Current.Request.Headers.GetValues("X-CorrelationId") != null ?
                        HttpContext.Current.Request.Headers.GetValues("X-CorrelationId").FirstOrDefault() : null
                    }
                };

                return correlationId;
            }

            return null;
        }
    }
}