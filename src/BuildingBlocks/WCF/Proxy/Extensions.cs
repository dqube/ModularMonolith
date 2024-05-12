namespace CompanyName.MyProjectName.BuildingBlocks.Wcf
{
    internal static class Extensions
    {
        public static TResult ExecuteWithRetry<T, TResult>(this IWcfProxy<T> proxy, Func<T, TResult> func, int retryCount = 3, int delay = 1000)
        {
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    return proxy.Execute(func);
                }
                catch (Exception ex)
                {
                    if (i == retryCount - 1)
                    {
                        throw;
                    }

                    // Log the exception or perform any other necessary actions
                    Console.WriteLine($"Retry {i + 1} failed with exception: {ex.Message}");

                    // Delay before the next retry
                    Thread.Sleep(delay);
                }
            }

            return default(TResult);
        }
    }
}