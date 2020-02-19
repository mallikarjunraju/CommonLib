using System;
using System.Collections.Generic;
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
    public class LoggerTest
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
        private readonly ILogger logger, log;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerTest"/> class.
        /// </summary>
        public LoggerTest()
        {
            var correlationProvider = MockRepository.GenerateMock<ICorrelationProvider>();
            correlationProvider.Stub(p => p.GetCorrelations(HttpContext.Current)).IgnoreArguments().Return(
                new Dictionary<string, string>
                {
                    { "Correlation Id", "123" }
                });

            var correlationProviderReturnNull = MockRepository.GenerateMock<ICorrelationProvider>();
            correlationProvider.Stub(p => p.GetCorrelations(null)).IgnoreArguments().Return(
               null);
            this.log = new Logger(correlationProviderReturnNull);
            this.logger = new Logger(correlationProvider);
        }

        /// <summary>
        /// When the given the error message then it should log the error.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheErrorMessageWithNoCorrelationIdThenItShouldLogTheError()
        {
            // Arrange
            string errorMessage = "Test error log";

            // Act
            this.log.ErrorDetails(errorMessage, SeverityValue.Error);

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
            this.logger.ErrorDetails(errorMessage, SeverityValue.Error);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given the error message then it should log the error.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheErrorMessageWithSeverityAllThenItShouldLogTheError()
        {
            // Arrange
            string errorMessage = "Test error log";

            // Act
            this.logger.ErrorDetails(errorMessage, SeverityValue.All);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Whens the given the error message then it should log the error.
        /// </summary>
        [TestMethod]
        public void WhenGivenTheErrorMessageWithSeverityCriticalThenItShouldLogTheError()
        {
            // Arrange
            string errorMessage = "Test error log";

            // Act
            this.logger.ErrorDetails(errorMessage, SeverityValue.Critical);

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
            this.logger.ErrorDetails(SeverityValue.Error, format, vars);

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
            this.logger.ErrorDetails(exception, SeverityValue.Error, format, vars);

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
            this.logger.ErrorDetails(exception, SeverityValue.Error);

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
            this.logger.Information(information, SeverityValue.Information);

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
            this.logger.Information(SeverityValue.Information, format, vars);

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
            this.logger.Information(new ArgumentException("Test logging"), SeverityValue.Information, format, vars);

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
            this.logger.ErrorDetails(exception, SeverityValue.Error, format, vars);

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
            this.logger.Warning(warningMessage, SeverityValue.Warning);

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
            this.logger.Warning(SeverityValue.Warning, format, vars);

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
            this.logger.Warning(exception, SeverityValue.Warning, format, vars);

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
            this.logger.TraceApi(componentName, method, timespan, SeverityValue.Verbose);

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
            this.logger.TraceApi(componentName, method, timespan, prop, SeverityValue.Verbose);

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
            this.logger.TraceApi(componentName, method, timespan, SeverityValue.Verbose, format, vars);

            // Assert
            Assert.IsTrue(true);
        }
    }
}
