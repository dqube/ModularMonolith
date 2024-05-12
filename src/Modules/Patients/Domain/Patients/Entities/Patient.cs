using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Kernel;
using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.ValueObjects;

namespace CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Entities;

internal class Patient : Aggregate<PatientId>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Patient()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private Patient(PatientId customerId, string name)
        : base(customerId)
    {
        Name = name;

        // Id= id;
    }

    public string Name { get; private set; }

    // public CustomerId Id { get; private set; }
    public static Patient Create(PatientId customerId, string name)
    {
        var customer = new Patient(customerId, name);
        return customer;
    }
}
