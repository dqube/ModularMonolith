using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Contracts;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Contracts
{
    public class IContractTests
    {
        [Fact]
        public void IContract_Type_ShouldReturnCorrectType()
        {
            // Arrange
            IContract contract = new SampleContract();

            // Act
            Type type = contract.Type;

            // Assert
            Assert.Equal(typeof(SampleContract), type);
        }

        [Fact]
        public void IContract_Required_ShouldReturnCorrectValues()
        {
            // Arrange
            IContract contract = new SampleContract();

            // Act
            IEnumerable<string> required = contract.Required;

            // Assert
            Assert.NotNull(required);
            Assert.Contains("Field1", required);
            Assert.Contains("Field2", required);
            Assert.Contains("Field3", required);
        }

        private class SampleContract : IContract
        {
            public Type Type => typeof(SampleContract);

            public IEnumerable<string> Required => new List<string> { "Field1", "Field2", "Field3" };
        }
    }
}
