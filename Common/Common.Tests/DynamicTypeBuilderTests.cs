using System;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class DynamicTypeBuilderTests
    {
        private IDynamicTypeBuilderFactory _sut;

        [SetUp]
        public void Before_each_test()
        {
            _sut = new DynamicTypeBuilderFactory();
        }

        [Test]
        public void Can_create_simple_type()
        {
            // Act
            var type = _sut.CreateTypeBuilder("MyTestTypeName").AddProperty<string>("StringProperty").AddProperty<int>("IntProperty").CreateType();

            // Assert
            Assert.IsNotNull(type);
            Assert.IsNotNull(type.GetProperty("StringProperty"));
            Assert.IsNotNull(type.GetProperty("IntProperty"));
            Assert.IsNull(type.GetProperty("NonExistentProperty"));
        }

        [Test]
        public void Can_create_multiple_types()
        {
            // Act
            var type1 = _sut.CreateTypeBuilder("MyTestTypeName1").AddProperty<string>("StringProperty").CreateType();
            var type2 = _sut.CreateTypeBuilder("MyTestTypeName2").AddProperty<string>("StringProperty").CreateType();

            // Assert
            Assert.IsNotNull(type1);
            Assert.IsNotNull(type2);
            Assert.AreNotEqual(type1, type2);
        }

        [Test]
        public void Can_create_instance_of_created_type()
        {
            // Arrange
            var type = _sut.CreateTypeBuilder("MyTestTypeName").AddProperty<string>("StringProperty").AddProperty<int>("IntProperty").CreateType();

            // Act
            dynamic instance = Activator.CreateInstance(type);

            // Assert
            Assert.IsNotNull(instance);
            Assert.IsNull(instance.StringProperty);
            Assert.AreEqual(0, instance.IntProperty);
        }

        [Test]
        public void Can_create_implementing_type()
        {
            // Arrange
            var type = _sut.CreateTypeBuilder("MyTestTypeName").AddProperty<string>("StringProperty").AddInterface<DynamicTypeTest>().CreateType();

            // Act
            var instance = (DynamicTypeTest)Activator.CreateInstance(type);
            instance.StringProperty = "test";

            // Assert
            Assert.AreEqual("test", instance.StringProperty);
        }

        public interface DynamicTypeTest
        {
            string StringProperty { get; set; }
        }
    }
}
