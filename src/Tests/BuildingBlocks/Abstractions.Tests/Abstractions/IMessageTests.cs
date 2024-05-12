using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Abstractions
{
    public class IMessageTests
    {
        private class TestMessage : IMessage
        {
        }

        private class TestMessageT : IMessage<string>
        {
        }

        [Fact]
        public void TestMessage_Implements_IMessage()
        {
            Assert.True(new TestMessage() is IMessage);
        }

        [Fact]
        public void TestMessageT_Implements_IMessageT()
        {
            Assert.True(new TestMessageT() is IMessage<string>);
        }
    }
}
