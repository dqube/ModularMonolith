using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Abstractions
{
    public class IDomainEventTests
    {
        private class TestDomainEvent : IDomainEvent
        {
        }

        [Fact]
        public void TestDomainEvent_Implements_IDomainEvent()
        {
            Assert.True(new TestDomainEvent() is IDomainEvent);
        }
    }
}
