using CompanyName.MyProjectName.Modules.Patients.Application.Clients.Users.DTO;

namespace CompanyName.MyProjectName.Modules.Patients.Application.Clients.Users;

internal interface IUserApiClient
{
    Task<UserDto> GetUserByMail(string email);

    Task<UserDto> GetUserAsync(int id);

    Task<IEnumerable<UserDto>> GetUsersAsync(int conferenceId);
}
