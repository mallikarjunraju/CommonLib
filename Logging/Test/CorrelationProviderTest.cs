using System;
using System.IO;
using System.Web;
using System.Web.SessionState;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Logging.Tests
{
    /// <summary>
    /// CorrelationProvider Test methods.
    /// </summary>
    [TestClass]
    public class CorrelationProviderTest
    {
        /// <summary>
        /// For given httpContext returning a string.
        /// </summary>
        [TestMethod]
        public void GivenHttpContextReturningString()
        {
            ICorrelationProvider correlationProvider = new CorrelationProvider();
            HttpContext context = FakeHttpContext("http://google.com");
            HttpContext.Current = FakeHttpContext("http://google.com");
            var corId = correlationProvider.GetCorrelations(context);
            Assert.IsNotNull(corId);
        }

        /// <summary>
        /// For given httpContext returning a null.
        /// </summary>
        [TestMethod]
        public void GivenHttpContextReturningNull()
        {
            ICorrelationProvider correlationProvider = new CorrelationProvider();
            var corId = correlationProvider.GetCorrelations(null);
            Assert.IsNull(corId);
        }

        /// <summary>
        /// Faking the HttpContext.
        /// </summary>
        /// <param name="url">The url for making HttpContext.</param>
        /// <returns> Return HttpContext.</returns>
        private static HttpContext FakeHttpContext(string url)
        {
            var uri = new Uri(url);
            var httpRequest = new HttpRequest(
                string.Empty,
                uri.ToString(),
                uri.Query.TrimStart('?'));
            using (var stringWriter = new StringWriter())
            {
                var httpResponse = new HttpResponse(stringWriter);
                var httpContext = new HttpContext(
                    httpRequest,
                    httpResponse);
                var sessionContainer = new HttpSessionStateContainer(
                    "id",
                    new SessionStateItemCollection(),
                    new HttpStaticObjectsCollection(),
                    10,
                    true,
                    HttpCookieMode.AutoDetect,
                    SessionStateMode.Custom,
                    false);
                SessionStateUtility.AddHttpSessionStateToContext(
                    httpContext,
                    sessionContainer);
                return httpContext;
            }
        }
    }
}
