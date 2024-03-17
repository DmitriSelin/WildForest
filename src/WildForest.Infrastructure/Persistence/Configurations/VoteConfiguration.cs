using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Ratings.Entities;
using WildForest.Domain.Ratings.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => VoteId.Create(value));

        builder
            .Property(p => p.Result)
            .IsRequired()
            .HasColumnName("Result");

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Votes)
            .HasForeignKey(x => x.UserId);

        builder
            .Property(p => p.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder
            .HasOne(x => x.Rating)
            .WithMany(x => x.Votes)
            .HasForeignKey(x => x.RatingId);

        builder
            .Property(p => p.RatingId)
            .HasConversion(
                id => id.Value,
                value => RatingId.Create(value));
    }
}