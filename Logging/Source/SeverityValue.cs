namespace Common.Logging
{
    /// <summary>
    /// Log categories which determines which severity levels should be logged.
    /// </summary>
    public enum SeverityValue
    {
        /// <summary>
        /// All categories.
        /// </summary>
        All,

        /// <summary>
        /// The critical levels.
        /// </summary>
        Critical,

        /// <summary>
        /// The error levels.
        /// </summary>
        Error,

        /// <summary>
        /// The information levels.
        /// </summary>
        Information,

        /// <summary>
        /// None of the severity levels will be logged.
        /// </summary>
        Off,

        /// <summary>
        /// The verbose levels.
        /// </summary>
        Verbose,

        /// <summary>
        /// The warning levels.
        /// </summary>
        Warning
    }
}
