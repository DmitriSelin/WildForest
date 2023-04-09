using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.WeatherMarks.Entities;
using WildForest.Domain.WeatherMarks.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class WeatherMarkConfiguration : IEntityTypeConfiguration<WeatherMark>
{
    public void Configure(EntityTypeBuilder<WeatherMark> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.ToString(),
                            value => WeatherMarkId.Parse(value));

        builder.OwnsOne(
            x => x.MediumMark,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasColumnName("MediumMark");
            });

        builder.HasOne(p => p.WeatherForecast)
            .WithMany(x => x.WeatherMarks)
            .HasForeignKey(p => p.WeatherId);

        builder.Property(x => x.WeatherId)
            .HasConversion(id => id.ToString(),
                            value => WeatherId.Parse(value));
    }
}