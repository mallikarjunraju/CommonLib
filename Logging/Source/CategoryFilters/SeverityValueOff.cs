using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging.CategoryFilters
{
    /// <summary>
    /// SeverityValueOff class.
    /// </summary>
    /// <seealso cref="Common.Logging.CategoryFilters.ICategoryFilter" />
    public class SeverityValueOff : ICategoryFilter
    {
        /// <summary>
        /// Determines whether the specified value is match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is match; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMatch(SeverityValue value)
        {
            return value == SeverityValue.Off;
        }

        /// <summary>
        /// Executes the specified current severity.
        /// </summary>
        /// <param name="currentSeverity">The current severity.</param>
        /// <returns>
        /// Returns the boolean value.
        /// </returns>
        public bool Execute(SeverityValue currentSeverity)
        {
            return false;
        }
    }
}
