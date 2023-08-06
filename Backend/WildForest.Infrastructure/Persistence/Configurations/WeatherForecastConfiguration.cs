using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        ConfigureWeatherForecasts(builder);
        ConfigureThreeHourWeatherForecasts(builder);
    }

    private static void ConfigureWeatherForecasts(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.ToTable("WeatherForecasts");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => WeatherForecastId.Create(value));

        builder
            .Property(p => p.Date)
            .IsRequired()
            .HasColumnType(ConfigurationSettings.Date)
            .HasColumnName("Date");

        builder
            .HasOne(p => p.City)
            .WithMany(x => x.WeatherForecasts)
            .HasForeignKey(p => p.CityId);

        builder
            .Property(x => x.CityId)
            .HasConversion(
                id => id.Value,
                value => CityId.Create(value));
    }

    private static void ConfigureThreeHourWeatherForecasts(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.OwnsMany(x => x.ThreeHourWeatherForecasts, sectionBuilder =>
        {
            sectionBuilder.ToTable("ThreeHourWeatherForecasts");

            sectionBuilder.HasKey(x => x.Id);

            sectionBuilder
                .Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ThreeHourWeatherForecastId.Create(value));

            sectionBuilder
                .Property(p => p.Time)
                .IsRequired()
                .HasColumnType(ConfigurationSettings.TimeWithoutTimeZone)
                .HasColumnName("Time");

            sectionBuilder.OwnsOne(
                x => x.Temperature,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasColumnName("Temperature");

                    sa.Property(p => p.ValueFeelsLike)
                    .HasColumnName("TemperatureFeelsLike");
                });

            sectionBuilder.OwnsOne(
                x => x.Pressure,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasColumnName("Pressure");
                });

            sectionBuilder.OwnsOne(
                x => x.Humidity,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasColumnName("Humidity");
                });

            sectionBuilder.OwnsOne(
                x => x.Description,
                sa =>
                {
                    sa.Property(p => p.Name)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("Name");

                    sa.Property(p => p.Description)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("Description");
                });

            sectionBuilder.OwnsOne(
                x => x.Cloudiness,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasColumnName("Cloudiness");
                });

            sectionBuilder.OwnsOne(
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

            sectionBuilder.OwnsOne(
                x => x.Visibility,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasColumnName("Visibility");
                });

            sectionBuilder.OwnsOne(
                x => x.PrecipitationProbability,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasColumnName("PrecipitationProbability");
                });

            sectionBuilder.OwnsOne(
                x => x.PrecipitationVolume,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasColumnName("PrecipitationVolume");
                });

            sectionBuilder
                .HasOne(p => p.WeatherForecast)
                .WithMany(x => x.ThreeHourWeatherForecasts)
                .HasForeignKey(p => p.WeatherForecastId);

            sectionBuilder
                .Property(x => x.WeatherForecastId)
                .HasConversion(
                    id => id.Value,
                    value => WeatherForecastId.Create(value));
        });

        builder.Metadata
            .FindNavigation(nameof(WeatherForecast.ThreeHourWeatherForecasts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}