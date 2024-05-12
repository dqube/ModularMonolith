using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Attributes
{
    public class MessageAttributeTests
    {
        [Message("TestExchange", "TestTopic", "TestQueue", "TestQueueType", "TestErrorQueue", "TestSubscriptionId", "TestModule", true)]
        private class TestClass
        {
        }

        [Fact]
        public void MessageAttribute_CanBeAppliedToClass()
        {
            var attributes = typeof(TestClass).GetCustomAttributes(typeof(MessageAttribute), false);
            Assert.NotEmpty(attributes);
        }

        [Fact]
        public void MessageAttribute_PropertiesCanBeSetAndRetrieved()
        {
            var attribute = new MessageAttribute("TestExchange", "TestTopic", "TestQueue", "TestQueueType", "TestErrorQueue", "TestSubscriptionId", "TestModule", true);

            Assert.Equal("TestExchange", attribute.Exchange);
            Assert.Equal("TestTopic", attribute.Topic);
            Assert.Equal("TestQueue", attribute.Queue);
            Assert.Equal("TestQueueType", attribute.QueueType);
            Assert.Equal("TestErrorQueue", attribute.ErrorQueue);
            Assert.Equal("TestSubscriptionId", attribute.SubscriptionId);
            Assert.Equal("TestModule", attribute.Module);
            Assert.True(attribute.Enabled);
        }
    }
}
