using System;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;

namespace Common.Tests.ExtensionMethods
{
    [TestFixture]
    public class EachTests
    {
        [SetUp]
        public void Before_each_test()
        {
            _array = Enumerable.Range(0, 1000000).ToArray();
        }

        private int[] _array;

        [Test]
        public void Each_returns_passed_array()
        {
            // Arrange

            // Act
            var result = _array.Each(x => { });

            // Assert
            Assert.AreSame(_array, result);
        }

        [Test]
        public void Each_returns_passed_list()
        {
            // Arrange
            var list = _array.ToList();

            // Act
            var result = list.Each(x => { });

            // Assert
            Assert.AreSame(list, result);
        }

        [Test]
        public void Each_enumerates_all_other_enumerables_and_returns_readonly_collection()
        {
            // Arrange
            var enumerated = false;
            var enumerable = _array.Where(x => enumerated = true);

            // Act
            var result = enumerable.Each(x => { });

            // Assert
            Assert.IsTrue(enumerated);
            Assert.AreNotSame(enumerable, result);
            Assert.IsInstanceOf<ReadOnlyCollection<int>>(result);


            // Second Arrange
            enumerated = false;

            // Second Act
            result.Each(x => { });

            // Second Assert
            Assert.IsFalse(enumerated);
        }
    }

    [TestFixture]
    public class EachWithIndexTests
    {
        [SetUp]
        public void Before_each_test()
        {
            _array = Enumerable.Range(0, 1000000).ToArray();
        }

        private int[] _array;

        [Test]
        public void Each_returns_passed_array()
        {
            // Arrange

            // Act
            var result = _array.Each((x, i) => { });

            // Assert
            Assert.AreSame(_array, result);
        }

        [Test]
        public void Each_returns_passed_list()
        {
            // Arrange
            var list = _array.ToList();

            // Act
            var result = list.Each((x, i) => { });

            // Assert
            Assert.AreSame(list, result);
        }

        [Test]
        public void Each_enumerates_all_other_enumerables_and_returns_readonly_collection()
        {
            // Arrange
            var enumerated = false;
            var enumerable = _array.Where(x => enumerated = true);

            // Act
            var result = enumerable.Each((x, i) => { });

            // Assert
            Assert.IsTrue(enumerated);
            Assert.AreNotSame(enumerable, result);
            Assert.IsInstanceOf<ReadOnlyCollection<int>>(result);


            // Second Arrange
            enumerated = false;

            // Second Act
            result.Each(x => { });

            // Second Assert
            Assert.IsFalse(enumerated);
        }
    }
}