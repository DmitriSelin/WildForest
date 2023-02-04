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

                    sa.Property(p => p.Description)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("Description");

                    sa.Property(p => p.Humidity)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("Humidity");

                    sa.Property(p => p.Pressure)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("Pressure");

                    sa.Property(p => p.Wind)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("Wind");

                    sa.Property(p => p.Cloudness)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("Cloudness");

                    sa.Property(p => p.Storm)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("Storm");
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