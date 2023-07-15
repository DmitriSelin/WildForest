using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.ToString(),
                            value => CityId.Parse(value));

        builder.OwnsOne(
            x => x.Name,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasColumnName("Name");
            });

        builder.OwnsOne(
            x => x.Location,
            sa =>
            {
                sa.Property(p => p.Latitude)
                .HasColumnName("Latitude");

                sa.Property(p => p.Longitude)
                .HasColumnName("Longitude");
            });

        builder.Metadata.FindNavigation(nameof(City.DayWeatherForecasts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(City.Users))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne(p => p.Country)
            .WithMany(x => x.Cities)
            .HasForeignKey(p => p.CountryId);

        builder.Property(x => x.CountryId)
            .HasConversion(id => id.ToString(),
                            value => CountryId.Parse(value));
    }
}