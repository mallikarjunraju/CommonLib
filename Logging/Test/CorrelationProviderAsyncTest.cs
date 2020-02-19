using System;
using System.IO;
using System.Web;
using System.Web.SessionState;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Logging.Tests
{
    /// <summary>
    /// CorrelationProviderAsync test methods.
    /// </summary>
    [TestClass]
    public class CorrelationProviderAsyncTest
    {
        /// <summary>
        /// Create an instance of ICorrelationProviderAsync. 
        /// </summary>
        private ICorrelationProviderAsync correlationProvider = new CorrelationProviderAsync();

        /// <summary>
        /// For given HttpContext returning a Null.
        /// </summary>
        [TestMethod]
        public void GivenHttpContextReturningNull()
        {
            var corId = this.correlationProvider.GetCorrelationsAsync(null);
            Assert.IsNull(corId.Result);
        }
    }
}
