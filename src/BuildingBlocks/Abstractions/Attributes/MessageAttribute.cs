namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class MessageAttribute : Attribute
{
    public string Exchange { get; }

    public string Topic { get; }

    public string Queue { get; }

    public string QueueType { get; }

    public string ErrorQueue { get; }

    public string SubscriptionId { get; }

    public string Module { get; }

    public bool Enabled { get; }

    public MessageAttribute(
        string exchange = null, string topic = null, string queue = null, string queueType = null, string errorQueue = null, string subscriptionId = null, string module = null, bool enabled = true)
    {
        Exchange = exchange ?? string.Empty;
        Topic = topic ?? string.Empty;
        Queue = queue ?? string.Empty;
        QueueType = queueType ?? string.Empty;
        ErrorQueue = errorQueue ?? string.Empty;
        SubscriptionId = subscriptionId ?? string.Empty;
        Module = module ?? string.Empty;
        Enabled = enabled;
    }
}
