using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Comments.Entities;
using WildForest.Domain.Comments.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CommentId.Create(value));

        builder
            .Property(p => p.Text)
            .HasMaxLength(200)
            .HasColumnName("Text");

        builder
            .Property(p => p.Date)
            .HasColumnType(ConfigurationSettings.TimeStampWithoutTimeZone)
            .HasColumnName("Date");

        builder
            .HasOne(p => p.WeatherForecast)
            .WithMany(x => x.Comments)
            .HasForeignKey(p => p.WeatherForecastId);

        builder
            .Property(p => p.WeatherForecastId)
            .HasConversion(
                id => id.Value,
                value => WeatherForecastId.Create(value));

        builder
            .HasOne(p => p.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(p => p.UserId);

        builder
            .Property(p => p.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
    }
}