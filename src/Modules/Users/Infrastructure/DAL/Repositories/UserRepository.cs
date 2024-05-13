using CompanyName.MyProjectName.Modules.Users.Domain.Users.Entities;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.Repositories;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyProjectName.Modules.Users.Infrastructure.DAL.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    private readonly DbSet<User> _patients;

    public UserRepository(UserDbContext context)
    {
        _context = context;
        _patients = _context.User;
    }

    public Task<User> GetAsync(UserId id, CancellationToken cancellationToken = default)
    {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        return _patients.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }

    public async Task AddAsync(User patient, CancellationToken cancellationToken = default)
    {
        await _patients.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User patient, CancellationToken cancellationToken = default)
    {
        _patients.Update(patient);
        await _context.SaveChangesAsync(cancellationToken);
    }
}