using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Exceptions;

namespace CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Exceptions;

public class PatientNotFoundException(int customerId) : CustomException($"Customer with ID: '{customerId}' was not found.")
{
    public int CustomerId { get; } = customerId;
}