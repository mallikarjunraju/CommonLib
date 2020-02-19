using System.Collections.Generic;
using System.Linq;
using Bupa.BGMU.Infrastructure.Logging.CategoryFilters;
namespace Bupa.BGMU.Infrastructure.Logging
{
    /// <summary>
    /// Category filter.
    /// </summary>
    public class CategoryFilter
    {
        /// <summary>
        /// Determines whether to log the current severity level or not.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="currentSeverity">The current severity.</param>
        /// <returns>
        /// Should be logged or not.
        /// </returns>
        /// Returns boolean value.
        public bool ShouldLogCurrentSeverity(SeverityValue value, SeverityValue currentSeverity)
        {
            switch (value)
            {
                case SeverityValue.All:
                case SeverityValue.Verbose:
                    return true;
                case SeverityValue.Critical:
                    return currentSeverity == SeverityValue.Critical;
                case SeverityValue.Error:
                    return (currentSeverity == SeverityValue.Critical || currentSeverity == SeverityValue.Error);
                case SeverityValue.Information:
                    return (currentSeverity != SeverityValue.Verbose);
                case SeverityValue.Warning:
                    return (currentSeverity == SeverityValue.Critical || currentSeverity == SeverityValue.Error || currentSeverity == SeverityValue.Warning);
                case SeverityValue.Off:
                default:
                    return false;
            }
        }
    }
}