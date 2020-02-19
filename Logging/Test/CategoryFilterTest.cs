using System.Collections.Generic;
using System.Linq;
using Common.Logging.CategoryFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Logging.Tests
{
    /// <summary>
    /// CategoryFilter test methods.
    /// </summary>
    [TestClass]
    public class CategoryFilterTest
    {
        /// <summary>
        /// The category filters.
        /// </summary>
        private List<ICategoryFilter> categoryFilters = new List<ICategoryFilter>
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
        /// Logging the current Severity for SeverityValue All.
        /// </summary>
        [TestMethod]
        public void GivenValueShouldLogCurrentSeverityForAll()
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(SeverityValue.All));
            if (filter != null)
            {
                Assert.IsTrue(filter.Execute(SeverityValue.All));
            }
        }

        /// <summary>
        /// Logging the current Severity for SeverityValue All.
        /// </summary>
        [TestMethod]
        public void GivenValueShouldLogCurrentSeverityForCriticalFalse()
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(SeverityValue.Critical));
            if (filter != null)
            {
                Assert.IsFalse(filter.Execute(SeverityValue.All));
            }
        }

        /// <summary>
        /// Logging the current Severity for SeverityValue All.
        /// </summary>
        [TestMethod]
        public void GivenValueShouldLogCurrentSeverityForCriticalTrue()
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(SeverityValue.Critical));
            if (filter != null)
            {
                Assert.IsTrue(filter.Execute(SeverityValue.Critical));
            }
        }

        /// <summary>
        /// Givens the value should log current severity for error.
        /// </summary>
        [TestMethod]
        public void GivenValueShouldLogCurrentSeverityForError()
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(SeverityValue.Error));
            if (filter == null)
            {
                return;
            }

            Assert.IsTrue(filter.Execute(SeverityValue.Error));
            Assert.IsTrue(filter.Execute(SeverityValue.Critical));
        }

        /// <summary>
        /// Givens the value should log current severity for information.
        /// </summary>
        [TestMethod]
        public void GivenValueShouldLogCurrentSeverityForInformation()
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(SeverityValue.Error));
            if (filter != null)
            {
                Assert.IsTrue(filter.Execute(SeverityValue.Error));
            }
        }

        /// <summary>
        /// Givens the value should log current severity for off.
        /// </summary>
        [TestMethod]
        public void GivenValueShouldLogCurrentSeverityForOff()
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(SeverityValue.Off));
            if (filter != null)
            {
                Assert.IsFalse(filter.Execute(SeverityValue.Error));
            }
        }

        /// <summary>
        /// Givens the value should log current severity for verbose.
        /// </summary>
        [TestMethod]
        public void GivenValueShouldLogCurrentSeverityForVerbose()
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(SeverityValue.Verbose));
            if (filter != null)
            {
                Assert.IsTrue(filter.Execute(SeverityValue.Error));
            }
        }

        /// <summary>
        /// Givens the value should log current severity for warning.
        /// </summary>
        [TestMethod]
        public void GivenValueShouldLogCurrentSeverityForWarning()
        {
            var filter = this.categoryFilters.FirstOrDefault(p => p.IsMatch(SeverityValue.Warning));
            if (filter == null)
            {
                return;
            }

            Assert.IsTrue(filter.Execute(SeverityValue.Critical));
            Assert.IsTrue(filter.Execute(SeverityValue.Error));
            Assert.IsTrue(filter.Execute(SeverityValue.Warning));
        }
    }
}