using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Logging.Tests
{
    /// <summary>
    /// Exception utilities test.
    /// </summary>
    [TestClass]
    public class ExceptionUtilitiesTest
    {
        /// <summary>
        /// Given The Exception Details Then Should Format The Exception.
        /// </summary>
        [TestMethod]
        public void GivenTheExceptionDetailsThenShouldFormatTheException()
        {
            var result = ExceptionUtilities.FormatException(new TimeoutException(), true);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Given The Exception Details Then Should Format The Exception.
        /// </summary>
        [TestMethod]
        public void GivenTheExceptionDetailsThenShouldFormatTheSqlException()
        {
            SqlException exp = this.MakeSqlException();
            var result = ExceptionUtilities.FormatException(exp, true);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Given The null Exception Then Should return null.
        /// </summary>
        [TestMethod]
        public void GivenTheNullExceptionThenShouldReturnEmptyString()
        {
            var result = ExceptionUtilities.FormatException(null, true);
            Assert.AreEqual(string.Empty, result);
        }

        /// <summary>
        /// Faking the SQLException.
        /// </summary>
        /// <returns> Return SQLException.</returns>
        private SqlException MakeSqlException()
        {
            SqlException exception = null;
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=.;Database=GUARANTEED_TO_FAIL;Connection Timeout=1");
                conn.Open();
            }
            catch (SqlException ex)
            {
                exception = ex;
            }

            return (exception);
        }
    }
}