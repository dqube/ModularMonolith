using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Attributes
{
    public class JobsAttributeTests
    {
        [Jobs("TestName", "TestFullName", "TestDescription", "TestTriggerName", "TestTriggerGroup")]
        private class TestClass
        {
        }

        [Fact]
        public void JobsAttribute_CanBeAppliedToClass()
        {
            var attributes = typeof(TestClass).GetCustomAttributes(typeof(JobsAttribute), false);
            Assert.NotEmpty(attributes);
        }

        [Fact]
        public void JobsAttribute_PropertiesCanBeSetAndRetrieved()
        {
            var attribute = new JobsAttribute("TestName", "TestFullName", "TestDescription", "TestTriggerName", "TestTriggerGroup");

            Assert.Equal("TestName", attribute.Name);
            Assert.Equal("TestFullName", attribute.FullName);
            Assert.Equal("TestDescription", attribute.Description);
            Assert.Equal("TestTriggerName", attribute.TriggerName);
            Assert.Equal("TestTriggerGroup", attribute.TriggerGroup);
            Assert.Equal(TimeSpan.Zero, attribute.Interval);
            Assert.Equal(DateTimeOffset.MinValue, attribute.StartAt);
            Assert.Equal(5, attribute.RepeatCount);
            Assert.Null(attribute.TriggerDescription);
            Assert.Equal(0, attribute.Priority);
            Assert.False(attribute.Manual);
        }
    }
}
