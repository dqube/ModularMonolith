using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Handlers;
using CompanyName.MyProjectName.Modules.Users.Application.Users.DTO;
using CompanyName.MyProjectName.Modules.Users.Application.Users.Queries;
using CompanyName.MyProjectName.Modules.Users.Infrastructure.DAL;
using CompanyName.MyProjectName.Modules.Users.Infrastructure.DAL.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyProjectName.Modules.Users.Infrastructure.Queries;

internal sealed class GetUserHandler(UserDbContext dbContext) : IQueryHandler<GetUser, UserDetailsDto>
{
    private readonly UserDbContext _dbContext = dbContext;

    public async Task<UserDetailsDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.User
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.UserId, cancellationToken);

#pragma warning disable CS8603 // Possible null reference return.
        return user?.AsDetailsDto();
#pragma warning restore CS8603 // Possible null reference return.
    }
}