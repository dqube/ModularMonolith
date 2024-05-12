namespace CompanyName.MyProjectName.BuildingBlocks.Transactions.Inbox;

public interface IInbox
{
    public bool Enabled { get; }

    Task HandleAsync(
        string messageId,
        string messageName,
        Func<Task> handler,
        CancellationToken cancellationToken = default);

    Task CleanupAsync(DateTime? to = null, CancellationToken cancellationToken = default);
}