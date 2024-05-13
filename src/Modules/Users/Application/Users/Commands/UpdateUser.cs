using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.Modules.Users.Application.Users.Commands;

internal record UpdateUser(int UserId, string Name) : ICommand
{
}
