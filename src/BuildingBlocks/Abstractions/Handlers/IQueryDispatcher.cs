using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;

public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}