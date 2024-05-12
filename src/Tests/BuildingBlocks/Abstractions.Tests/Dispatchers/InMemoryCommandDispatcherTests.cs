using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Dispatchers;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Tests.Dispatchers
{
    public class InMemoryCommandDispatcherTests
    {
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly CommandDispatcher _commandDispatcher;

        public InMemoryCommandDispatcherTests()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
            _commandDispatcher = new CommandDispatcher(_serviceProviderMock.Object);
        }

        [Fact]
        public async Task SendAsync_WithValidCommand_ShouldInvokeCommandHandler()
        {
            // Arrange
            var command = new TestCommand();
            var commandHandlerMock = new Mock<ICommandHandler<TestCommand>>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            _serviceProviderMock.Setup(x => x.GetService(typeof(ICommandHandler<TestCommand>)))
                .Returns(new TestCommand());

            // Act
            await _commandDispatcher.SendAsync(command);

            // Assert
            commandHandlerMock.Verify(x => x.HandleAsync(command, default), Times.Once);
        }

        [Fact]
        public async Task SendAsync_WithNullCommand_ShouldThrowInvalidOperationException()
        {
            // Arrange
            TestCommand command = null;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _commandDispatcher.SendAsync(command));
        }

        [Fact]
        public async Task SendAsync_WithValidCommandAndCancellation_ShouldInvokeCommandHandlerWithCancellation()
        {
            // Arrange
            var command = new TestCommand();
#pragma warning disable SA1129 // Do not use default value type constructor
            var cancellationToken = new CancellationToken();
#pragma warning restore SA1129 // Do not use default value type constructor
            var commandHandlerMock = new Mock<ICommandHandler<TestCommand>>();
            _serviceProviderMock.Setup(x => x.GetService(typeof(ICommandHandler<TestCommand>)))
                .Returns(commandHandlerMock.Object);

            // Act
            await _commandDispatcher.SendAsync(command, cancellationToken);

            // Assert
            commandHandlerMock.Verify(x => x.HandleAsync(command, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task SendAsync_WithValidCommandAndResult_ShouldInvokeCommandHandlerWithResult()
        {
            // Arrange
            var command = new TestCommandWithResult();
            var commandHandlerMock = new Mock<ICommandHandler<TestCommandWithResult>>();
            _serviceProviderMock.Setup(x => x.GetService(typeof(ICommandHandler<TestCommandWithResult>)))
                .Returns(commandHandlerMock.Object);

            // Act
            await _commandDispatcher.SendAsync(command);

            // Assert
            commandHandlerMock.Verify(x => x.HandleAsync(command, default), Times.Once);
        }

        [Fact]
        public async Task SendAsync_WithNullCommandAndResult_ShouldThrowInvalidOperationException()
        {
            // Arrange
            TestCommandWithResult command = null;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _commandDispatcher.SendAsync(command));
        }

        [Fact]
        public async Task SendAsync_WithValidCommandAndResultAndCancellation_ShouldInvokeCommandHandlerWithResultAndCancellation()
        {
            // Arrange
            var command = new TestCommandWithResult();
#pragma warning disable SA1129 // Do not use default value type constructor
            var cancellationToken = new CancellationToken();
#pragma warning restore SA1129 // Do not use default value type constructor
            var commandHandlerMock = new Mock<ICommandHandler<TestCommandWithResult>>();
            _serviceProviderMock.Setup(x => x.GetService(typeof(ICommandHandler<TestCommandWithResult>)))
                .Returns(commandHandlerMock.Object);

            // Act
            await _commandDispatcher.SendAsync(command, cancellationToken);

            // Assert
            commandHandlerMock.Verify(x => x.HandleAsync(command, cancellationToken), Times.Once);
        }

        private class TestCommand : ICommand
        {
        }

        private class TestCommandWithResult : ICommand<int>
        {
        }
    }
}
