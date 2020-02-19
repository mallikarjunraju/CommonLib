using System.Collections.Generic;
using System.Linq;
using Common.Logging.CategoryFilters;

namespace Common.Logging
{
    /// <summary>
    /// Category filter.
    /// </summary>
    public class CategoryFilter
    {
        private readonly List<ICategoryFilter> categoryFiltersList = new List<ICategoryFilter>
                              {
                                  new SeverityValueAll(),
                                  new SeverityValueCritical(),
                                  new SeverityValueError(),
                                  new SeverityValueInformation(),
                                  new SeverityValueOff(),
                                  new SeverityValueVerbose(),
                                  new SeverityValueWarning()
                              };

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
            ICategoryFilter categoryFilter = categoryFiltersList.FirstOrDefault(p => p.IsMatch(value));
            if (categoryFilter != null)
            {
                return categoryFilter.Execute(currentSeverity);
            }

            return false;           
        }
    }
}