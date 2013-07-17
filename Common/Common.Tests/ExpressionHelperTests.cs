using System;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class ExpressionHelperTests
    {
        [Test]
        public void Can_get_memberexpression_name()
        {
            Assert.AreEqual("PropertyOne", ExpressionHelper.GetName<TestClass>(x => x.PropertyOne));
        }

        [Test]
        public void Can_get_unaryexpression_name()
        {
            Assert.AreEqual("PropertyTwo", ExpressionHelper.GetName<TestClass>(x => x.PropertyTwo));
        }

        private class TestClass
        {
            public string PropertyOne { get; set; }
            public Guid PropertyTwo { get; set; }
        }
    }
}
