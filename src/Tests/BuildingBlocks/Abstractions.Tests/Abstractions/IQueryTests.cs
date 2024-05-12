using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using Xunit;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Abstractions
{
    public class IQueryTests
    {
        private class TestQuery : IQuery<string>
        {
        }

        [Fact]
        public void TestQuery_Implements_IQuery()
        {
            Assert.True(new TestQuery() is IQuery<string>);
        }
    }
}
