namespace CompanyName.MyProjectName.BuildingBlocks.Contexts;

#nullable enable

public interface IContext
{
    string ActivityId { get; }

    string TraceId { get; }

    string CorrelationId { get; }

    string? MessageId { get; }

    string? CausationId { get; }

    string? UserId { get; }
}
