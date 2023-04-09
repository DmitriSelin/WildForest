using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(id => id.ToString(),
                                value => UserId.Parse(value));

            builder.OwnsOne(
                x => x.FirstName,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("FirstName");
                });

            builder.OwnsOne(
                x => x.LastName,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("LastName");
                });

            builder.Property(x => x.Role)
                .HasColumnName("Role");

            builder.OwnsOne(
                x => x.Email,
                sa =>
                {
                    sa.Property(p => p.Value)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("Email");
                });

            builder.OwnsOne(
                x => x.Password, 
                sa => 
                { 
                    sa.Property(p => p.Value)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("Password");
                });
            
            builder.Metadata.FindNavigation(nameof(User.RefreshTokens))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(p => p.City)
                .WithMany(x => x.Users)
                .HasForeignKey(p => p.CityId);

            builder.Property(p => p.CityId)
                .HasConversion(id => id.ToString(),
                                value => CityId.Parse(value));
            
            builder.Metadata.FindNavigation(nameof(User.Marks))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}