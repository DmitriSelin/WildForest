using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public class MarkConfiguration : IEntityTypeConfiguration<Mark>
{
    public void Configure(EntityTypeBuilder<Mark> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.ToString(),
                        value => MarkId.Parse(value));

        builder.OwnsOne(
            x => x.Rating, 
            sa =>
            {
                sa.Property(p => p.Value)
                    .HasColumnName("Rating");
            });

        builder.OwnsOne(
            x => x.Comment,
            sa =>
            {
                sa.Property(p => p.Value)
                    .HasMaxLength(200)
                    .HasColumnName("Comment");
            });

        builder.OwnsOne(
            x => x.Date,
            sa =>
            {
                sa.Property(p => p.Value)
                .HasColumnName("Date");
            });

        builder.HasOne(p => p.User)
            .WithMany(x => x.Marks)
            .HasForeignKey(p => p.UserId);

        builder.Property(x => x.UserId)
            .HasConversion(id => id.ToString(),
                        value => UserId.Parse(value));

        builder.HasOne(p => p.WeatherForecast)
            .WithMany(x => x.Marks)
            .HasForeignKey(p => p.WeatherId);

        builder.Property(x => x.WeatherId)
            .HasConversion(id => id.ToString(),
                value => WeatherId.Parse(value));
    }
}