using System.Collections.Generic;
using System.Web;

namespace Common.Logging
{
    /// <summary>
    /// Correlational details provider.
    /// </summary>
    public interface ICorrelationProvider
    {
        /// <summary>
        /// Gets the correlations.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Header details that has to be logged.</returns>
        IDictionary<string, string> GetCorrelations(HttpContext context);
    }
}
