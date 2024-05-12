using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.Entities;
using CompanyName.MyProjectName.Modules.Patients.Domain.Patients.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyProjectName.Modules.Patients.Infrastructure.DAL.Configurations;

internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        // builder.HasIndex(x => new { x.OwnerId, x.Currency }).IsUnique();
        // builder.Property(x => x.Version).IsConcurrencyToken();
        // builder.HasOne<Owner>().WithMany().HasForeignKey(x => x.OwnerId);
        builder.ToTable("Patients", "patients");
        builder.HasKey(e => e.Id);
        builder.Property(x => x.Id)
            .HasColumnName(nameof(PatientId))
            .HasConversion(x => x.Value, x => new PatientId(x))
            .ValueGeneratedOnAdd();
    }
}