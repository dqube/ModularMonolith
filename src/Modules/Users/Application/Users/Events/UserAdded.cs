using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.Modules.Users.Application.Users.Events;

internal record UserAdded(int UserId, string Name) : IEvent;