using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Marks;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class MarkConfiguration : IEntityTypeConfiguration<Mark>
{
    public void Configure(EntityTypeBuilder<Mark> builder)
    {
        ConfigureMarks(builder);
        ConfigureVotes(builder);
    }

    private static void ConfigureMarks(EntityTypeBuilder<Mark> builder)
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
    }

    private static void ConfigureVotes(EntityTypeBuilder<Mark> builder)
    {
        builder.OwnsMany(x => x.Votes, sectionBuilder =>
        {
            sectionBuilder.ToTable("Votes");

            sectionBuilder.HasKey(x => x.Id);

            sectionBuilder
                .Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => VoteId.Create(value));

            sectionBuilder
                .Property(p => p.Result)
                .IsRequired()
                .HasColumnName("Result");

            sectionBuilder
                .HasOne(x => x.User)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.UserId);

            sectionBuilder
                .Property(p => p.UserId)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value));

            sectionBuilder
                .HasOne(x => x.Mark)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.MarkId);

            sectionBuilder
                .Property(p => p.MarkId)
                .HasConversion(
                    id => id.Value,
                    value => MarkId.Create(value));
        });

        builder.Metadata
            .FindNavigation(nameof(Mark.Votes))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}