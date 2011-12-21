using System;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;

namespace Common.Tests.ExtensionMethods
{
    [TestFixture]
    public class GetDisplayNameTests
    {
        [Test]
        public void Can_get_name_of_property()
        {
            // Arrange

            // Act
            var name = typeof(TestClass).GetMember("Property")[0].GetDisplayName();

            // Assert
            Assert.AreEqual("Property", name);
        }

        [Test]
        public void Can_get_name_of_property_with_display_attr()
        {
            // Arrange

            // Act
            var name = typeof(TestClass).GetMember("Property2")[0].GetDisplayName();

            // Assert
            Assert.AreEqual("Property Two", name);
        }

        [Test]
        public void Can_get_name_of_enum_value()
        {
            // Arrange

            // Act
            var name = TestEnum.Value1.GetDisplayName();

            // Assert
            Assert.AreEqual("Value1", name);
        }

        [Test]
        public void Can_get_name_of_enum_value_with_display_attr()
        {
            // Arrange

            // Act
            var name = TestEnum.Value2.GetDisplayName();

            // Assert
            Assert.AreEqual("Value Two", name);
        }

        private class TestClass
        {
            public string Property { get; set; }

            [Display(Name = "Property Two")]
            public string Property2 { get; set; }
        }

        private enum TestEnum
        {
            Value1,
            [Display(Name = "Value Two")] Value2
        }
    }
}