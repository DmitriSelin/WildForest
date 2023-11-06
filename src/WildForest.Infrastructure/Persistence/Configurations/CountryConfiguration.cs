using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CountryId.Create(value));

        builder.OwnsOne(
            x => x.Name,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasMaxLength(ConfigurationSettings.MaxStringLength)
                .HasColumnName("Name");
            });

        builder.Metadata
            .FindNavigation(nameof(Country.Cities))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}