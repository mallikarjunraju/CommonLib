namespace Common.Logging.CategoryFilters
{
    /// <summary>
    /// SeverityValueVerbose class.
    /// </summary>
    /// <seealso cref="Common.Logging.CategoryFilters.ICategoryFilter" />
    public class SeverityValueVerbose : ICategoryFilter
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
            return value == SeverityValue.Verbose;
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
