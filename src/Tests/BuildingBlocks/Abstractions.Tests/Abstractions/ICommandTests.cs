using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using Xunit;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Abstractions
{
    public class ICommandTests
    {
        private class TestCommand : ICommand
        {
        }

        private class TestCommandT : ICommand<string>
        {
        }

        [Fact]
        public void TestCommand_Implements_ICommand()
        {
            Assert.True(new TestCommand() is ICommand);
        }

        [Fact]
        public void TestCommandT_Implements_ICommandT()
        {
            Assert.True(new TestCommandT() is ICommand<string>);
        }
    }
}
