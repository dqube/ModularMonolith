namespace CompanyName.MyProjectName.Modules.Patients.Domain.Patients.ValueObjects;

internal readonly record struct PatientId
{
    public int Value { get; }

    public PatientId(int value)
    {
        Value = value;
    }

    public static implicit operator int(PatientId customerId)
        => customerId.Value;

    public static implicit operator PatientId(int value)
        => new(value);

    public override string ToString() => Value.ToString();
}
