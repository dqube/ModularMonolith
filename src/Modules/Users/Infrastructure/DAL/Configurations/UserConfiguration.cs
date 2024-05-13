using CompanyName.MyProjectName.Modules.Users.Domain.Users.Entities;
using CompanyName.MyProjectName.Modules.Users.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyProjectName.Modules.Users.Infrastructure.DAL.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // builder.HasIndex(x => new { x.OwnerId, x.Currency }).IsUnique();
        // builder.Property(x => x.Version).IsConcurrencyToken();
        // builder.HasOne<Owner>().WithMany().HasForeignKey(x => x.OwnerId);
        builder.ToTable("User", "patients");
        builder.HasKey(e => e.Id);
        builder.Property(x => x.Id)
            .HasColumnName(nameof(UserId))
            .HasConversion(x => x.Value, x => new UserId(x))
            .ValueGeneratedOnAdd();
    }
}