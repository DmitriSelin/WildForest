using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Ratings;
using WildForest.Domain.Ratings.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => RatingId.Create(value));

        builder
            .Property(p => p.Points)
            .IsRequired()
            .HasColumnName("Points");

        builder
            .HasOne(x => x.WeatherForecast)
            .WithOne(x => x.Rating)
            .HasForeignKey<Rating>(x => x.WeatherForecastId)
            .IsRequired();

        builder
            .Property(p => p.WeatherForecastId)
            .HasConversion(
                id => id.Value,
                value => WeatherForecastId.Create(value));

        builder.Metadata
            .FindNavigation(nameof(Rating.Votes))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}