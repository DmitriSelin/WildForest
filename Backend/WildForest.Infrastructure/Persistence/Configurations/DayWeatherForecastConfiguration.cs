using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class DayWeatherForecastConfiguration : IEntityTypeConfiguration<DayWeatherForecast>
{
    public void Configure(EntityTypeBuilder<DayWeatherForecast> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.ToString(),
                            value => WeatherForecastId.Parse(value));

        builder.Property(p => p.Date)
            .IsRequired()
            .HasColumnType(ConfigurationSettings.Date)
            .HasColumnName("Date");

        builder.HasOne(p => p.City)
            .WithMany(x => x.DayWeatherForecasts)
            .HasForeignKey(p => p.CityId);

        builder.Property(x => x.CityId)
            .HasConversion(id => id.ToString(),
                            value => CityId.Parse(value));

        builder.Metadata.FindNavigation(nameof(DayWeatherForecast.ThreeHourWeatherForecasts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}