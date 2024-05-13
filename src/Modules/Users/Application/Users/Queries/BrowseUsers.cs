using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Pagination;
using CompanyName.MyProjectName.Modules.Users.Application.Users.DTO;

namespace CompanyName.MyProjectName.Modules.Users.Application.Users.Queries;

internal class BrowseUsers : PagedQuery<UserDto>
{
    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;
}