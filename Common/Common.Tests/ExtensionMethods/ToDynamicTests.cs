using System;
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
            Assert.IsInstanceOf<TestClass2>(sut.Property);
            Assert.AreEqual("test", sut.Property.Property);
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