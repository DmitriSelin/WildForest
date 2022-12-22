using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Common.Persistence.Configurations
{
    public class DayWeatherConfiguration : IEntityTypeConfiguration<DayWeather>
    {
        public void Configure(EntityTypeBuilder<DayWeather> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(id => id.ToString(),
                                value => WeatherId.Parse(value));

            builder.Property(x => x.Date)
                .HasColumnName("ForecastDate")
                .HasColumnType("date");

            builder.Property(x => x.DaySpan)
                .HasColumnName("DaySpan");

            builder.OwnsOne(
                x => x.WeatherDetails,
                sa =>
                {
                    sa.Property(p => p.Temperature)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
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
        }
    }
}