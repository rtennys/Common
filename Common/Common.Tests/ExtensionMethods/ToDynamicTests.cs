using System;
using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;

namespace Common.Tests.ExtensionMethods
{
    [TestFixture]
    public class ToDynamicTests
    {
        [Test]
        public void Can_create_from_simple_anonymous()
        {
            Assert.AreEqual("test", new {Property = "test"}.ToDynamic().Property);
            Assert.AreEqual(1, new {Property = 1}.ToDynamic().Property);
        }

        [Test]
        public void Can_create_from_more_complex()
        {
            var sut = new {Property = new {Property = "test"}}.ToDynamic();

            Assert.IsInstanceOf<ExpandoObject>(sut);
            Assert.IsInstanceOf<ExpandoObject>(sut.Property);
            Assert.AreEqual("test", sut.Property.Property);
        }

        [Test]
        public void Can_create_from_real_types()
        {
            var sut = new TestClass1 {Property = new TestClass2 {Property = "test"}}.ToDynamic();

            Assert.IsInstanceOf<ExpandoObject>(sut);
            Assert.IsInstanceOf<ExpandoObject>(sut.Property);
            Assert.AreEqual("test", sut.Property.Property);
        }

        [Test]
        public void Can_create_with_arrays()
        {
            // Arrange
            var input = new[] {1, 2, 3};

            // Act
            var sut = input.ToDynamic();

            // Assert
            Assert.IsInstanceOf<IEnumerable<dynamic>>(sut);
            Assert.AreEqual(3, sut.Count);
            Assert.AreEqual(1, sut[0]);
        }

        [Test]
        public void Can_create_with_anonymous_objects_containing_arrays()
        {
            // Arrange
            var input = new
                {
                    Property = "test",
                    Array = new[]
                        {
                            new {Property = 1},
                            new {Property = 2},
                            new {Property = 3},
                        }
                };

            // Act
            var sut = input.ToDynamic();

            // Assert
            Assert.IsInstanceOf<ExpandoObject>(sut);
            Assert.IsInstanceOf<IEnumerable<dynamic>>(sut.Array);
            Assert.AreEqual(3, sut.Array.Count);
            Assert.AreEqual(1, sut.Array[0].Property);
        }

        private class TestClass1
        {
            public TestClass2 Property { get; set; }
        }

        private class TestClass2
        {
            public string Property { get; set; }
        }
    }
}