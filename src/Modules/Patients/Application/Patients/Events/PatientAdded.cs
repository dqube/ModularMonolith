using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.Modules.Patients.Application.Patients.Events;

internal record PatientAdded(int PatientId, string Name) : IEvent;