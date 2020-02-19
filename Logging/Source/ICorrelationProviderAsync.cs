using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Web;

namespace Common.Logging
{
    /// <summary>
    /// Async Correctional details provider.
    /// </summary>
    public interface ICorrelationProviderAsync
    {
        /// <summary>
        /// Gets the correlations.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Header details that has to be logged.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an async method.")]
        Task<Dictionary<string, string>> GetCorrelationsAsync(HttpContext context);
    }
}
