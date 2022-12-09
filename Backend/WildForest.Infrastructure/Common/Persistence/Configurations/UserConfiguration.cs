using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Infrastructure.Common.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(id => id.ToString(),
                                value => UserId.Parse(value));

            builder.Property(x => x.FirstName)
                .HasMaxLength(ConfigurationSettings.MaxStringLength);

            builder.Property(x => x.LastName)
                .HasMaxLength(ConfigurationSettings.MaxStringLength);

            builder.Property(x => x.Role);

            builder.Property(x => x.Email)
                .HasMaxLength(ConfigurationSettings.MaxStringLength);

            builder.Property(x => x.Password)
                .HasMaxLength(ConfigurationSettings.MaxStringLength);

            builder.HasOne(x => x.City)
                .WithMany(x => x.Users);

            builder.Property(x => x.CityId)
                .HasConversion(id => id.ToString(),
                                value => CityId.Parse(value));
        }
    }
}