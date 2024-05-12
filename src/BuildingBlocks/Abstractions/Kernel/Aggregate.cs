using System.ComponentModel.DataAnnotations.Schema;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Kernel;

public abstract class Aggregate<TId> : Entity<TId>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    public new TId Id { get; protected set; }

    protected Aggregate()
    {
    }

    protected Aggregate(TId id)
        : base(id)
    {
        Id = id;
    }
}