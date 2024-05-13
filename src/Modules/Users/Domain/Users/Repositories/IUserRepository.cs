using CompanyName.MyProjectName.Modules.Users.Domain.Users.Entities;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.ValueObjects;

namespace CompanyName.MyProjectName.Modules.Users.Domain.Users.Repositories;

internal interface IUserRepository
{
    Task<User> GetAsync(UserId id, CancellationToken cancellationToken = default);

    Task AddAsync(User user, CancellationToken cancellationToken = default);

    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
}
