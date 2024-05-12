using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.BuildingBlocks.SQLServer;

namespace CompanyName.MyProjectName.BuildingBlocks.Transactions.Decorators;

[Decorator]
internal sealed class TransactionalCommandHandlerDecorator<T> : ICommandHandler<T>
    where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionalCommandHandlerDecorator(ICommandHandler<T> handler, IUnitOfWork unitOfWork)
    {
        _handler = handler;
        _unitOfWork = unitOfWork;
    }

    public Task HandleAsync(T command, CancellationToken cancellationToken = default)
        => _unitOfWork.ExecuteAsync(() => _handler.HandleAsync(command, cancellationToken), cancellationToken);
}