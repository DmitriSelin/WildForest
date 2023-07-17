using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Votes.Entities;
using WildForest.Domain.Votes.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .HasConversion(id => id.Value,
                            value => VoteId.Create(value));

        builder.Property(p => p.Points)
            .IsRequired()
            .HasColumnName("Points");

        builder.HasOne(p => p.User)
            .WithMany(x => x.Votes)
            .HasForeignKey(p => p.UserId);

        builder.Property(p => p.UserId)
            .HasConversion(id => id.Value,
                            value => UserId.Create(value));

        builder.HasOne(x => x.DayWeatherForecast)
            .WithOne(x => x.Vote)
            .HasForeignKey<Vote>(x => x.DayWeatherForecastId)
            .IsRequired();
    }
}