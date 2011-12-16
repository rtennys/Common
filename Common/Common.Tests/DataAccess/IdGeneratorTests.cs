using System;
using Common.DataAccess;
using NUnit.Framework;

namespace Common.Tests.DataAccess
{
    namespace IdGeneratorTests
    {
        [TestFixture]
        public class When_getting_new_ids
        {
            [Test]
            public void Should_get_next_hi_on_first_pull()
            {
                var _nextHi = 0;
                IdGenerator.Initialize(() => _nextHi++);

                Assert.AreEqual(1, IdGenerator.NextId());
                Assert.AreEqual(1, _nextHi);
            }

            [Test]
            public void Should_not_get_next_hi_until_out_of_low()
            {
                var _nextHi = 0;
                IdGenerator.Initialize(() => _nextHi++);

                for (var nextId = 1; nextId <= IdGenerator.MaxLo; nextId++)
                {
                    Assert.AreEqual(nextId, IdGenerator.NextId());
                    Assert.AreEqual(1, _nextHi);
                }

                Assert.AreEqual(IdGenerator.MaxLo + 1, IdGenerator.NextId());
                Assert.AreEqual(2, _nextHi);

                Assert.AreEqual(IdGenerator.MaxLo + 2, IdGenerator.NextId());
                Assert.AreEqual(2, _nextHi);
            }
        }
    }
}