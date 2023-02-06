using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations
{
    public sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(id => id.ToString(),
                                value => CountryId.Parse(value));

            builder.OwnsOne(
                x => x.CountryName,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasColumnName("CountryName");
                });
        }
    }
}