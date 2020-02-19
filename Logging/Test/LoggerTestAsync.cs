using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Common.Logging.CategoryFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Common.Logging.Tests
{
    /// <summary>
    /// Logger test methods.
    /// </summary>
    [TestClass]
    public class LoggerTestAsync
    {
        /// <summary>
        /// The category filters.
        /// </summary>
        private readonly List<ICategoryFilter> categoryFilters = new List<ICategoryFilter>
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
        /// The logger.
        /// </summary>
        private readonly ILoggerAsync loggerAsync, logAsync;

        /// <summary>
        /// Initialises a new instance of the <see cref="LoggerTestAsync"/> class.
        /// </summary>
        public LoggerTestAsync()
        {
            var correlationProvider = MockRepository.GenerateMock<ICorrelationProviderAsync>();
            /*//correlationProvider.Stub(p => p.GetCorrelations(HttpContext.Current)).IgnoreArguments().Return(
            //    new Dictionary<string, string>
            //    {
            //        { "Correlation Id", "123" }
            //    });*/
            var correlations = new Dictionary<string, string>
                {
                    { "Correlation Id", "123" }
                };
            correlationProvider.Stub(p => p.GetCorrelationsAsync(HttpContext.Current)).IgnoreArguments().Return(
               Task.FromResult<Dictionary<string, string>>(correlations));

            var correlationProviderReturnsNull = MockRepository.GenerateMock<ICorrelationProviderAsync>();
            correlationProvider.Stub(p => p.GetCorrelationsAsync(HttpContext.Current)).IgnoreArguments().Return(
               null);

            this.logAsync = new LoggerAsync(correlationProviderReturnsNull);
            this.loggerAsync = new LoggerAsync(correlationProvider);
        }

        /// <summary>
        /// Whens the given the error message then it should log the error.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheErrorMessageWithNoCorrelationIdThenItShouldLogTheError()
        {
            // Arrange
            string errorMessage = "Test error log";

            // Act
            this.logAsync.ErrorDetailsAsync(errorMessage, SeverityValue.Error);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given the error message then it should log the error.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheErrorMessageThenItShouldLogTheError()
        {
            // Arrange
            string errorMessage = "Test error log";

            // Act
            this.loggerAsync.ErrorDetailsAsync(errorMessage, SeverityValue.Error);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given input details in error scenario then should log error.
        /// </summary>
        [TestMethod]
        public void WhenGivenInputDetailsInErrorScenarioThenShouldLogError()
        {
            // Arrange
            var format = "\r\n-----------\r\n{0}";
            object[] vars = new object[] { "input1" };

            // Act
            this.loggerAsync.ErrorDetailsAsync(SeverityValue.Error, format, vars);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given the exception then should log the exception.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheExceptionThenShouldLogTheException()
        {
            // Arrange
            var exception = new ArgumentException("Test logging");
            var format = "\r\n-----------\r\n{0}";
            object[] vars = new object[] { "input1" };

            // Act
            this.loggerAsync.ErrorDetailsAsync(exception, SeverityValue.Error, format, vars);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the give only the exception then should log exception details.
        /// </summary>
        [TestMethod]
        public void WhenGiveOnlyTheExceptionThenShouldLogExceptionDetails()
        {
            // Arrange
            var exception = new ArgumentException("Test exception");

            // Act
            this.loggerAsync.ErrorDetailsAsync(exception, SeverityValue.Error);

            // Assert.
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given the information message then should log the information.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheInformationMessageThenShouldLogTheInformation()
        {
            // Arrange
            string information = "Test information log";

            // Act
            this.loggerAsync.InformationAsync(information, SeverityValue.Information);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given the input details should log information.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheInputDetailsShouldLogInformation()
        {
            // Arrange
            var format = "\r\n-----------\r\n{0}";
            object[] vars = new object[] { "Input object" };

            // Act
            this.loggerAsync.InformationAsync(SeverityValue.Information, format, vars);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given the exception details along with input details should log information.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheExceptionInputDetailsShouldLogInformation()
        {
            // Arrange
            var format = "\r\n-----------\r\n{0}";
            object[] vars = new object[] { "Input object" };

            // Act
            this.loggerAsync.InformationAsync(new ArgumentException("Test logging"), SeverityValue.Information, format, vars);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given the information with exception then should log the exception.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheInformationWithExceptionThenShouldLogTheException()
        {
            // Arrange
            var exception = new ArgumentException("Test logging");
            var format = "\r\n-----------\r\n{0}";
            object[] vars = new object[] { "Information exception" };

            // Act
            this.loggerAsync.ErrorDetailsAsync(exception, SeverityValue.Error, format, vars);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given a warning message should log warning.
        /// </summary>
        [TestMethod]
        public void WhenGivenAWarningMessageShouldLogWarning()
        {
            // Arrange
            var warningMessage = "Test Log warning";

            // Act
            this.loggerAsync.WarningAsync(warningMessage, SeverityValue.Warning);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given the input details should log warning.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheInputDetailsShouldLogWarning()
        {
            // Arrange
            var format = "\r\n-----------\r\n{0}";
            object[] vars = new object[] { "Warning1" };

            // Act
            this.loggerAsync.WarningAsync(SeverityValue.Warning, format, vars);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given the warning details with exception then should log the warning.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheWarningDetailsWithExceptionThenShouldLogTheWarning()
        {
            // Arrange
            var exception = new ArgumentException("Test logging");
            var format = "\r\n-----------\r\n{0}";
            object[] vars = new object[] { "Exception warning" };

            // Act
            this.loggerAsync.WarningAsync(exception, SeverityValue.Warning, format, vars);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given component details should log API trace.
        /// </summary>
        [TestMethod]
        public void WhenGivenComponentDetailsShouldLogApiTrace()
        {
            // Arrange
            var componentName = "TraceApi";
            var method = "TestLog";
            var timespan = new TimeSpan(23, 09, 04);

            // Act
            this.loggerAsync.TraceApiAsync(componentName, method, timespan, SeverityValue.Verbose);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given component details with properties should log API trace.
        /// </summary>
        [TestMethod]
        public void WhenGivenComponentDetailsWithPropertiesShouldLogApiTrace()
        {
            // Arrange
            var componentName = "TraceApi";
            var method = "TestLog";
            var timespan = new TimeSpan(23, 09, 04);
            var prop = "prop1";

            // Act
            this.loggerAsync.TraceApiAsync(componentName, method, timespan, prop, SeverityValue.Verbose);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given components with object array should log API trace.
        /// </summary>
        [TestMethod]
        public void WhenGivenComponentsWithObjectArrayShouldLogApiTrace()
        {
            // Arrange
            var componentName = "TraceApi";
            var method = "TestLog";
            var timespan = new TimeSpan(23, 09, 04);
            var format = "\r\n-----------\r\n{0}";
            object[] vars = new object[] { "Trace api" };

            // Act
            this.loggerAsync.TraceApiAsync(componentName, method, timespan, SeverityValue.Verbose, format, vars);

            // Assert
            Assert.IsTrue(true);
        }
    }
}
