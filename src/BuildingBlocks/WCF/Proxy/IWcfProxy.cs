namespace CompanyName.MyProjectName.BuildingBlocks.Wcf;

public interface IWcfProxy<out T> : IDisposable
{
    void Execute(Action<T> action);

    TResult Execute<TResult>(Func<T, TResult> func);

    Task ExecuteAsync(Func<T, Task> func);

    Task<TResult> ExecuteAsync<TResult>(Func<T, Task<TResult>> func);

    void ExecuteWithRetry(Action<T> action, int retryCount = 3, int delay = 1000);
}
