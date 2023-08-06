using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Marks;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class MarkConfiguration : IEntityTypeConfiguration<Mark>
{
    public void Configure(EntityTypeBuilder<Mark> builder)
    {
        builder.ToTable("Marks");

        builder.HasKey(x => x.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MarkId.Create(value));

        builder
            .Property(p => p.Points)
            .IsRequired()
            .HasColumnName("Points");

        builder
            .HasOne(x => x.WeatherForecast)
            .WithOne(x => x.Mark)
            .HasForeignKey<Mark>(x => x.WeatherForecastId)
            .IsRequired();

        builder
            .Property(p => p.WeatherForecastId)
            .HasConversion(
                id => id.Value,
                value => WeatherForecastId.Create(value));

        builder.Metadata
            .FindNavigation(nameof(Mark.Votes))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}