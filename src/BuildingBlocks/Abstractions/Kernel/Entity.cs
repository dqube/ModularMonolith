using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Kernel;

public abstract class Entity<TId> : IEntity, IEquatable<Entity<TId>>
    where TId : notnull
{
    private readonly Collection<IDomainEvent> _domainEvents = new();

    protected Entity()
    {
    }

    protected Entity(TId id)
    {
        Id = id;
        _domainEvents = new Collection<IDomainEvent>();
    }

    [NotMapped]
    public int DomainEventVersion { get; protected set; }

    public long Version { get; set; } = -1;

    private bool _versionIncremented;

    public TId Id { get; protected set; }

    [JsonIgnore]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public virtual bool Equals(Entity<TId> other)
    {
        return Equals((object)other);
    }

    public override bool Equals(object obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void AddDomainEvent(IDomainEvent @event)
    {
        if (!_domainEvents.Any() && !_versionIncremented)
        {
            DomainEventVersion++;
            _versionIncremented = true;
        }

        _domainEvents.Add(@event);
    }

    public void RemoveDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Remove(@event);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void IncrementVersion()
    {
        if (_versionIncremented)
        {
            return;
        }

        DomainEventVersion++;
        _versionIncremented = true;
    }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public long? LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; }
}
