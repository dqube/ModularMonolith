#nullable enable
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Attributes
{
    public class HiddenAttributeTests
    {
        private class TestClass
        {
            [Hidden]
            public string? TestProperty { get; set; }
        }

        [Fact]
        public void TestProperty_Has_HiddenAttribute()
        {
            var propertyInfo = typeof(TestClass).GetProperty("TestProperty");
            var attributes = propertyInfo!.GetCustomAttributes(typeof(HiddenAttribute), false);
            Assert.NotEmpty(attributes);
        }
    }
}
