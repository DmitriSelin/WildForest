using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Infrastructure.Common.Persistence.Configurations
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
                property =>
                {
                    property.Property(p => p.Country)
                    .HasColumnName("CountryName")
                    .HasMaxLength(ConfigurationSettings.MaxStringLength);

                    property.Property(p => p.Latitude).HasColumnName("Latitude");
                    property.Property(p => p.Longitude).HasColumnName("Longitude");
                });
        }
    }
}