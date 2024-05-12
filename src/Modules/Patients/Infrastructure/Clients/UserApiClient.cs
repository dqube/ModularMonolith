using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Modules;
using CompanyName.MyProjectName.Modules.Patients.Application.Clients.Users;
using CompanyName.MyProjectName.Modules.Patients.Application.Clients.Users.DTO;
using CompanyName.MyProjectName.Modules.Patients.Infrastructure.Clients.Requests;

namespace CompanyName.MyProjectName.Modules.Patients.Infrastructure.Clients;

internal sealed class UserApiClient(IModuleClient client) : IUserApiClient
{
    private readonly IModuleClient _client = client;

    public Task<IEnumerable<UserDto>> GetUsersAsync(int userId)
     => _client.SendAsync<IEnumerable<UserDto>>("users/users/get", new GetUser
     {
         UserId = userId
     });

    public Task<UserDto> GetUserAsync(int id)
    => _client.SendAsync<UserDto>(
        "users/get",
        new GetUser
                {
                    UserId = id
                });

    public Task<UserDto> GetUserByMail(string email)
   => _client.SendAsync<UserDto>("users/getbymail", new { email });
}
