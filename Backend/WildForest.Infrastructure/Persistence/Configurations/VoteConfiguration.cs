using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.ToTable("Votes");

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
            .HasOne(x => x.Mark)
            .WithMany(x => x.Votes)
            .HasForeignKey(x => x.MarkId);

        builder
            .Property(p => p.MarkId)
            .HasConversion(
                id => id.Value,
                value => MarkId.Create(value));
    }
}