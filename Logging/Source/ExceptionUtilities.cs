using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    /// <summary>
    /// This helper class is used to format the exception details before logging.
    /// </summary>
    internal static class ExceptionUtilities
    {
        /// <summary>
        /// The default formatter, By default return Exception.ToString().
        /// </summary>
        private static readonly Func<Exception, string> defaultFormatter =
            (ex) => ex == null ? string.Empty : ex.ToString();

        #region "Specialized Formatters"

        /// <summary>
        /// Keep a map of specialized formatters for types with extra embedded
        /// information.
        /// </summary>
        private static readonly IDictionary<Type, Func<Exception, string>>
            FormatExceptionMap = new Dictionary<Type, Func<Exception, string>>
            {
                { typeof(SqlException), (ex) => FormatSqlException(ex) }
            };

        #endregion

        /// <summary>
        /// Formats the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="includeContext">If set to <c>true</c> [include context].</param>
        /// <returns>
        /// Formatted exception.
        /// </returns>
        public static string FormatException(
            Exception exception,
            bool includeContext)
        {
            if (exception == null)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            try
            {
                // Whether or not to include the application and machine context.
                if (includeContext)
                {
                    AppendContext(stringBuilder);
                }

                AppendExceptionInfo(stringBuilder, exception);
            }
            catch (FormatException ex)
            {
                stringBuilder.AppendFormat("Warning; Could not format exception {0}", ex.ToString());
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Formats the SQL exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>Formatted SQL exception.</returns>
        private static string FormatSqlException(Exception ex)
        {
            var sqlEx = ex as SqlException;
            if (sqlEx == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            sb.AppendLine(ex.ToString());
            for (int i = 0; i < sqlEx.Errors.Count; i++)
            {
                var sqlErr = sqlEx.Errors[i];
                sb.AppendFormat(
                    "[#{0}] Message: {0}, LineNumber: {1}, Source: {2}, Procedure: {3}\r\n",
                    sqlErr.Number,
                    sqlErr.Message,
                    sqlErr.LineNumber,
                    sqlErr.Source,
                    sqlErr.Procedure);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Appends the exception information.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        /// <param name="exception">The exception.</param>
        private static void AppendExceptionInfo(
            StringBuilder builder,
            Exception exception)
        {
            Func<Exception, string> formatter = defaultFormatter;
            if (FormatExceptionMap.ContainsKey(exception.GetType()))
            {
                formatter = FormatExceptionMap[exception.GetType()];
            }

            builder.AppendFormat(
                "\r\n------------------------------\r\n{0}",
                formatter(exception));
        }

        /// <summary>
        /// Appends the context.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        private static void AppendContext(StringBuilder builder)
        {
            var currentAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            var lastWritten = File.GetLastWriteTime(currentAssembly.Location);

            builder.AppendFormat(
                "[Context] assembly={0},version={1},buildTime={2},appDomain={3},basePath={4}",
                currentAssembly.FullName,
                currentAssembly.GetName().Version.ToString(),
                lastWritten,
                AppDomain.CurrentDomain.FriendlyName,
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        }
    }
}
