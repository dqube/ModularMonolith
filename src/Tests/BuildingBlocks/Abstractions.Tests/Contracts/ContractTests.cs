using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Contracts;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Contracts
{
    public class ContractTests
    {
        private class TestContract : IContract
        {
            public Type Type => typeof(string);

            public IEnumerable<string> Required => new List<string> { "Test1", "Test2" };
        }

        [Fact]
        public void TestContract_Implements_IContract()
        {
            Assert.True(new TestContract() is IContract);
        }

        [Fact]
        public void TestContract_TypeProperty_ReturnsCorrectType()
        {
            var contract = new TestContract();
            Assert.Equal(typeof(string), contract.Type);
        }

        [Fact]
        public void TestContract_RequiredProperty_ReturnsCorrectValues()
        {
            var contract = new TestContract();
            Assert.Contains("Test1", contract.Required);
            Assert.Contains("Test2", contract.Required);
        }
    }
}
