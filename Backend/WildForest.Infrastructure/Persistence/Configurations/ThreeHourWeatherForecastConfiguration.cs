using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class ThreeHourWeatherForecastConfiguration : IEntityTypeConfiguration<ThreeHourWeatherForecast>
{
    public void Configure(EntityTypeBuilder<ThreeHourWeatherForecast> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.ToString(),
                            value => WeatherForecastId.Parse(value));

        builder.Property(p => p.Time)
            .IsRequired()
            .HasColumnType(ConfigurationSettings.TimeWithoutTimeZone)
            .HasColumnName("Time");

        builder.OwnsOne(
            x => x.Temperature,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasColumnName("Temperature");

                sa.Property(p => p.ValueFeelsLike)
                .HasColumnName("TemperatureFeelsLike");
            });

        builder.OwnsOne(
            x => x.Pressure,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasColumnName("Pressure");
            });

        builder.OwnsOne(
            x => x.Humidity,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasColumnName("Humidity");
            });

        builder.OwnsOne(
            x => x.WeatherDescription,
            sa =>
            {
                sa.Property(p => p.Name)
                .HasMaxLength(ConfigurationSettings.MaxStringLength)
                .HasColumnName("Name");

                sa.Property(p => p.Description)
                .HasMaxLength(ConfigurationSettings.MaxStringLength)
                .HasColumnName("Description");
            });

        builder.OwnsOne(
            x => x.Cloudiness,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasColumnName("Cloudiness");
            });

        builder.OwnsOne(
            x => x.Wind,
            sa =>
            {
                sa.Property(p => p.Speed)
                .HasColumnName("WindSpeed");

                sa.Property(p => p.Direction)
                .HasColumnName("WindDirection");

                sa.Property(p => p.Gust)
                .HasColumnName("WindGust");
            });

        builder.OwnsOne(
            x => x.Visibility,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasColumnName("Visibility");
            });

        builder.OwnsOne(
            x => x.PrecipitationProbability,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasColumnName("PrecipitationProbability");
            });

        builder.OwnsOne(
            x => x.PrecipitationVolume,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasColumnName("PrecipitationVolume");
            });

        builder.HasOne(p => p.DayWeatherForecast)
            .WithMany(x => x.ThreeHourWeatherForecasts)
            .HasForeignKey(p => p.DayWeatherForecastId);

        builder.Property(x => x.DayWeatherForecastId)
            .HasConversion(id => id.ToString(),
                            value => WeatherForecastId.Parse(value));
    }
}