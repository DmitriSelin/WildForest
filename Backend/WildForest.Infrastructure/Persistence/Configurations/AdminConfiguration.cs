using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Admins.Entites;
using WildForest.Domain.Admins.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.ToString(),
                            value => AdminId.Parse(value));

        builder.OwnsOne(
            x => x.FirstName,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasMaxLength(ConfigurationSettings.MaxStringLength)
                .HasColumnName("FirstName");
            });

        builder.OwnsOne(
            x => x.LastName,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasMaxLength(ConfigurationSettings.MaxStringLength)
                .HasColumnName("LastName");
            });

        builder.Property(x => x.Role)
            .IsRequired()
            .HasColumnName("Role");

        builder.OwnsOne(
            x => x.Email,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasMaxLength(ConfigurationSettings.MaxStringLength)
                .HasColumnName("Email");
            });

        builder.OwnsOne(
            x => x.Password,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasMaxLength(ConfigurationSettings.MaxStringLength)
                .HasColumnName("Password");
            });

        builder.Metadata.FindNavigation(nameof(Admin.RefreshTokens))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}