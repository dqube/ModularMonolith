using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.Modules.Patients.Application.Patients.DTO;

namespace CompanyName.MyProjectName.Modules.Patients.Application.Patients.Queries;

internal class GetPatient : IQuery<PatientDetailsDto>
{
    public int PatientId { get; set; }
}