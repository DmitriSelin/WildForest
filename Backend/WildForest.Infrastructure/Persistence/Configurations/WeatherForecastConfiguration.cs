using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations
{
    public sealed class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(id => id.ToString(),
                                value => WeatherId.Parse(value));

            builder.OwnsOne(
                x => x.ForecastDate,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasColumnName("ForecastDate");
                });

            builder.OwnsOne(
                x => x.ForecastTime,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasColumnName("ForecastTime");
                });

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
                    .HasColumnName("WeatherName");

                    sa.Property(p => p.Description)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("WeatherDescription");
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
                    .IsRequired(false)
                    .HasColumnName("PrecipitationVolume");
                });

            builder.HasOne(p => p.City)
                .WithMany(x => x.WeatherForecasts)
                .HasForeignKey(p => p.CityId);

            builder.Property(x => x.CityId)
                .HasConversion(id => id.ToString(),
                                value => CityId.Parse(value));
            
            builder.Metadata.FindNavigation(nameof(WeatherForecast.Marks))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata.FindNavigation(nameof(WeatherForecast.WeatherMarks))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}