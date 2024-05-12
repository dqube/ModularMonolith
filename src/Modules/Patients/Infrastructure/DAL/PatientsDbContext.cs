using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyProjectName.Modules.Patients.Infrastructure.DAL;

internal class PatientsDbContext(DbContextOptions<PatientsDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("patients");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
