using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.Modules.Patients.Application.Patients.Commands;

internal record UpdatePatient(int PatientId, string Name) : ICommand
{
}
