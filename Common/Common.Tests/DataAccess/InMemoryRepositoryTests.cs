using System;
using System.Linq;
using Common.DataAccess;
using NUnit.Framework;

namespace Common.Tests.DataAccess
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void Equals_works_as_advertised()
        {
            var entity1 = new MyEntity();
            var entity2 = new MyEntity();

            Assert.AreEqual(entity1, entity1);
            Assert.AreNotEqual(entity1, entity2);

            entity1.Id = 1;
            Assert.AreNotEqual(entity1, entity2);
            entity2.Id = 1;
            Assert.AreEqual(entity1, entity2);
        }

        private class MyEntity : Entity<int>
        {
            public new int Id
            {
                set { base.Id = value; }
            }
        }
    }

    [TestFixture]
    public class InMemoryRepositoryTests
    {
        private InMemoryRepository _repository;

        [TestFixtureSetUp]
        public void before_all_test()
        {
            _repository = new InMemoryRepository();
        }

        [Test]
        public void Add_should_add_to_data_store()
        {
            // Arrange
            var myEntity = new MyEntity {PropertyOne = "test"};

            // Act
            _repository.Add(myEntity);
            var entity = _repository.Get<MyEntity>(myEntity.Id);
            var entity2 = _repository.Find<MyEntity>(x => x.PropertyOne == "test").SingleOrDefault();

            // Assert
            Assert.AreSame(myEntity, entity);
            Assert.AreSame(entity, entity2);
        }

        private class MyEntity : Entity<Guid>
        {
            public virtual string PropertyOne { get; set; }
        }
    }
}