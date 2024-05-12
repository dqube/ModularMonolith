namespace CompanyName.MyProjectName.BuildingBlocks.SQLServer;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default);
}