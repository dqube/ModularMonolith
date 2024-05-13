using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.ValueObjects;

namespace CompanyName.MyProjectName.Modules.Users.Application.Users.Commands;

internal record AddUser(UserId UserId, string Name) : ICommand
{
}
