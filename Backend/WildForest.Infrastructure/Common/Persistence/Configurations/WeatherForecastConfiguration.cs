using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Common.Persistence.Configurations
{
    public sealed class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(id => id.ToString(),
                                value => WeatherId.Parse(value));

            builder.Property(x => x.Date)
                .HasColumnName("ForecastDate")
                .HasColumnType("date");

            builder.Property(x => x.Time)
                .HasColumnName("DayTime");

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
                });

            builder.Property(x => x.CityId)
                .IsRequired()
                .HasColumnName("CityId");

            builder.Property(x => x.CityId)
                .HasConversion(id => id.ToString(),
                                value => CityId.Parse(value));
        }
    }
}