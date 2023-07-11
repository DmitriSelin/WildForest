using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Votes.Entities;
using WildForest.Domain.Votes.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .HasConversion(id => id.ToString(),
                            value => VoteId.Parse(value));

        builder.Property(p => p.Points)
            .IsRequired()
            .HasColumnName("Points");
    }
}