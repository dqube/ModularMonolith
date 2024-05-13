using CompanyName.MyProjectName.Modules.Users.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyProjectName.Modules.Users.Infrastructure.DAL;

internal class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("patients");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
