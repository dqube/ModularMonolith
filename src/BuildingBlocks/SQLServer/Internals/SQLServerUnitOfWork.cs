using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyProjectName.BuildingBlocks.SQLServer.Internals;

internal sealed class SQLServerUnitOfWork<T> : IUnitOfWork
    where T : DbContext
{
    private readonly T _dbContext;

    public SQLServerUnitOfWork(T dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await action();
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }
}