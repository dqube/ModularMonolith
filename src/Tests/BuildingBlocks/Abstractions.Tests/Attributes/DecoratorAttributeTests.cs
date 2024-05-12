using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Attributes
{
    public class DecoratorAttributeTests
    {
        [Decorator]
        private class TestClass
        {
        }

        [Fact]
        public void TestClass_Has_DecoratorAttribute()
        {
            var attributes = typeof(TestClass).GetCustomAttributes(typeof(DecoratorAttribute), false);
            Assert.NotEmpty(attributes);
        }
    }
}
