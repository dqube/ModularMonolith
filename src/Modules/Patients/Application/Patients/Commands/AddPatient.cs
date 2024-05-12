using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.ValueObjects;

namespace CompanyName.MyProjectName.Modules.Patients.Application.Patients.Commands;

internal record AddPatient(PatientId PatientId, string Name) : ICommand
{
}
