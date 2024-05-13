namespace CompanyName.MyProjectName.Modules.Users.Domain.Users.ValueObjects;

internal readonly record struct UserId
{
    public int Value { get; }

    public UserId(int value)
    {
        Value = value;
    }

    public static implicit operator int(UserId customerId)
        => customerId.Value;

    public static implicit operator UserId(int value)
        => new(value);

    public override string ToString() => Value.ToString();
}
