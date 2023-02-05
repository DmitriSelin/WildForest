using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations
{
    public sealed class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(id => id.ToString(),
                                value => CityId.Parse(value));

            builder.Property(x => x.Name)
                .HasColumnName("CityName")
                .HasMaxLength(ConfigurationSettings.MaxStringLength);

            builder.OwnsOne(
                x => x.Location,
                sa =>
                {
                    sa.Property(p => p.Latitude).HasColumnName("Latitude");
                    sa.Property(p => p.Longitude).HasColumnName("Longitude");
                });

            builder.Property(x => x.CountryId)
                .IsRequired()
                .HasColumnName("CountryId");

            builder.Property(x => x.CountryId)
                .HasConversion(id => id.ToString(),
                                value => CountryId.Parse(value));
        }
    }
}