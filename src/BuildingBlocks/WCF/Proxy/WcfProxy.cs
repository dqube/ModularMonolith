using System.ServiceModel;

namespace CompanyName.MyProjectName.BuildingBlocks.Wcf;

public class WcfProxy<T> : IWcfProxy<T>
        where T : class, ICommunicationObject
{
    private T _channel;

    public WcfProxy(string endpointUrl)
    {
        if (string.IsNullOrWhiteSpace(endpointUrl))
        {
            throw new ArgumentException("Endpoint configuration name cannot be null or empty.", nameof(endpointUrl));
        }

        _channel = CreateChannel(endpointUrl);
    }

    public void Dispose()
    {
        if (_channel != null && _channel.State != CommunicationState.Closed)
        {
            try
            {
                _channel.Close();
            }
            catch (CommunicationException)
            {
                _channel.Abort();
            }
            catch (TimeoutException)
            {
                _channel.Abort();
            }
            catch (Exception)
            {
                _channel.Abort();
                throw;
            }
        }
    }

    public void Execute(Action<T> action)
    {
        action?.Invoke(_channel);
    }

    public TResult Execute<TResult>(Func<T, TResult> func)
    {
        return func != null ? func(_channel) : default;
    }

    public async Task ExecuteAsync(Func<T, Task> func)
    {
        if (func != null)
        {
            await func(_channel);
        }
    }

    public async Task<TResult> ExecuteAsync<TResult>(Func<T, Task<TResult>> func)
    {
        return func != null ? await func(_channel) : default;
    }

    public async void ExecuteWithRetry(Action<T> action, int retryCount, int delay)
    {
        for (int i = 0; i < retryCount; i++)
        {
            try
            {
                action?.Invoke(_channel);
                return;
            }
            catch (Exception)
            {
                if (i == retryCount - 1)
                {
                    throw;
                }

                await Task.Delay(delay);
            }
        }
    }

    public async Task<TResult> ExecuteWithRetryAsync<TResult>(Func<T, TResult> func, int retryCount, int delay)
    {
        for (int i = 0; i < retryCount; i++)
        {
            try
            {
                return func != null ? func(_channel) : default;
            }
            catch (Exception)
            {
                if (i == retryCount - 1)
                {
                    throw;
                }

                await Task.Delay(delay);
            }
        }

        return default;
    }

    private static T CreateChannel(string endpointConfigurationName)
    {
        var binding = new BasicHttpBinding();
        var factory = new ChannelFactory<T>(binding, new EndpointAddress(endpointConfigurationName));
        return factory.CreateChannel();
    }
}