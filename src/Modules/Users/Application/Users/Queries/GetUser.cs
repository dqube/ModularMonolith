using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.Modules.Users.Application.Users.DTO;

namespace CompanyName.MyProjectName.Modules.Users.Application.Users.Queries;

internal class GetUser : IQuery<UserDetailsDto>
{
    public int UserId { get; set; }
}