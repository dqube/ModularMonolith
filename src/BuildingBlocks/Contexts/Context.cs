using System.Diagnostics;

namespace CompanyName.MyProjectName.BuildingBlocks.Contexts;
#nullable enable

public sealed class Context : IContext
{
    public string ActivityId { get; }

    public string TraceId { get; }

    public string CorrelationId { get; }

    public string? MessageId { get; }

    public string? CausationId { get; }

    public string? UserId { get; }

    public Context()
    {
        ActivityId = Activity.Current?.Id ?? ActivityTraceId.CreateRandom().ToString();
        TraceId = string.Empty;
        CorrelationId = Guid.NewGuid().ToString("N");
    }

    public Context(
        string activityId,
        string traceId,
        string correlationId,
        string? messageId = null,
        string? causationId = null,
        string? userId = null)
    {
        if (string.IsNullOrEmpty(activityId))
        {
            throw new ArgumentException($"'{nameof(activityId)}' cannot be null or empty.", nameof(activityId));
        }

        if (string.IsNullOrEmpty(traceId))
        {
            throw new ArgumentException($"'{nameof(traceId)}' cannot be null or empty.", nameof(traceId));
        }

        if (string.IsNullOrEmpty(correlationId))
        {
            throw new ArgumentException($"'{nameof(correlationId)}' cannot be null or empty.", nameof(correlationId));
        }

        ActivityId = activityId;
        TraceId = traceId;
        CorrelationId = correlationId;
        MessageId = messageId;
        CausationId = causationId;
        UserId = userId;
    }
}