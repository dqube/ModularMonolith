using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Abstractions
{
    public class IEventTests
    {
        private class TestEvent : IEvent
        {
        }

        [Fact]
        public void TestEvent_Implements_IEvent()
        {
            Assert.True(new TestEvent() is IEvent);
        }

        [Fact]
        public void TestEvent_Implements_IMessage()
        {
            Assert.True(new TestEvent() is IMessage);
        }
    }
}
