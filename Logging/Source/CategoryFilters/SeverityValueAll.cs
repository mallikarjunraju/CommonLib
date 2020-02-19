namespace Common.Logging.CategoryFilters
{
    /// <summary>
    /// SeverityValueAll class.
    /// </summary>
    /// <seealso cref="Common.Logging.CategoryFilters.ICategoryFilter" />
    public class SeverityValueAll : ICategoryFilter
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
            return value == SeverityValue.All;
        }

        /// <summary>
        /// Executes the specified value.
        /// </summary>
        /// <param name="currentSeverity">The current severity.</param>
        /// <returns>Returns true always.</returns>
        public bool Execute(SeverityValue currentSeverity)
        {
            return true;
        }
    }
}
