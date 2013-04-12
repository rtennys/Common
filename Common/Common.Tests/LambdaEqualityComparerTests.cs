using System;
using System.Linq;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class LambdaEqualityComparerTests
    {
        [Test]
        public void Can_union_two_lists()
        {
            // Arrange
            var first = new[] {new TestObject("a", "a"), new TestObject("a", "b")};
            var second = new[] {new TestObject("a", "b"), new TestObject("b", "a")};

            // Act
            var result = first.Union(second, (x, y) => x.One == y.One && x.Two == y.Two, x => (x.One ?? "").GetHashCode() ^ (x.Two ?? "").GetHashCode()).ToArray();

            // Assert
            Assert.AreEqual(3, result.Length);
        }

        [Test]
        public void Can_union_two_lists_with_overriden_gethashcode()
        {
            // Arrange
            var first = new[] {new Test2Object("a", "a"), new Test2Object("a", "b")};
            var second = new[] {new Test2Object("a", "b"), new Test2Object("b", "a")};

            // Act
            var result = first.Union(second, (x, y) => x.One == y.One && x.Two == y.Two).ToArray();

            // Assert
            Assert.AreEqual(3, result.Length);
        }

        private class TestObject
        {
            public TestObject(string one, string two)
            {
                One = one;
                Two = two;
            }

            public string One { get; set; }
            public string Two { get; set; }
        }

        private class Test2Object
        {
            public Test2Object(string one, string two)
            {
                One = one;
                Two = two;
            }

            public string One { get; set; }
            public string Two { get; set; }

            public override int GetHashCode()
            {
                return One.GetHashCode() ^ Two.GetHashCode();
            }
        }
    }
}
