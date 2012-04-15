using System;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class EnumHelperTests
    {
        [Test]
        public void IsDefined_is_not_case_sensitive()
        {
            // Arrange

            // Act
            var result = EnumHelper.IsDefined<Test>("one");

            // Assert
            Assert.IsTrue(result);
        }

        private enum Test
        {
            One,
            Two
        }
    }
}