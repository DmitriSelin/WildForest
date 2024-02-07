using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Languages.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

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

        builder
            .Property(x => x.Role)
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

        builder
            .HasAlternateKey(x => x.Email);

        builder.OwnsOne(
            x => x.Password,
            sa =>
            {
                sa.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("Password");

                sa.Property("_salt")
                .IsRequired()
                .HasColumnName("Salt");
            });

        builder
            .HasOne(p => p.City)
            .WithMany(x => x.Users)
            .HasForeignKey(p => p.CityId);

        builder
            .Navigation(x => x.City)
            .AutoInclude();

        builder
            .Property(p => p.CityId)
            .HasConversion(
                id => id.Value,
                value => CityId.Create(value));

        builder
            .HasOne(p => p.Language)
            .WithMany(x => x.Users)
            .HasForeignKey(p => p.LanguageId);

        builder
            .Navigation(x => x.Language)
            .AutoInclude();

        builder
            .Property(p => p.LanguageId)
            .HasConversion(
                id => id.Value,
                value => LanguageId.Create(value));

        builder.Metadata
            .FindNavigation(nameof(User.RefreshTokens))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(User.Votes))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(User.Comments))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}