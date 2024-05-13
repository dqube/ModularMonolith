using CompanyName.MyProjectName.Modules.Users.Application.Users.DTO;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.Entities;

namespace CompanyName.MyProjectName.Modules.Users.Infrastructure.DAL.Mappings;

internal static class UserExtensions
{
    public static UserDto AsDto(this User user)
        => user.Map<UserDto>();

    public static UserDetailsDto AsDetailsDto(this User user)
    {
        var dto = user.Map<UserDetailsDto>();

        return dto;
    }

    private static T Map<T>(this User user)
        where T : UserDto, new()
        => new()
        {
            UserId = user.Id,
            Name = user.Name,

            // CreatedAt = user.CreatedAt
        };
}