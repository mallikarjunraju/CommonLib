namespace Common.Logging.CategoryFilters
{
    /// <summary>
    /// ICategoryFilter interface.
    /// </summary>
    public interface ICategoryFilter
    {
        /// <summary>
        /// Determines whether the specified value is match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is match; otherwise, <c>false</c>.
        /// </returns>
        bool IsMatch(SeverityValue value);

        /// <summary>
        /// Executes the specified current severity.
        /// </summary>
        /// <param name="currentSeverity">The current severity.</param>
        /// <returns>
        /// Returns the boolean value.
        /// </returns>
        bool Execute(SeverityValue currentSeverity);
    }
}
