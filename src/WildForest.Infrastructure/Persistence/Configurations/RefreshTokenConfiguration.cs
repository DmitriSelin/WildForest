using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => RefreshTokenId.Create(value));

        builder
            .Property(p => p.Token)
            .IsRequired()
            .HasColumnName("Token");

        builder
            .Property(p => p.Expiration)
            .IsRequired()
            .HasColumnType(ConfigurationSettings.TimeStampWithTimeZone)
            .HasColumnName("Expiration");

        builder
            .Property(p => p.CreationDate)
            .IsRequired()
            .HasColumnType(ConfigurationSettings.TimeStampWithTimeZone)
            .HasColumnName("CreationDate");

        builder
            .Property(p => p.CreatedByIp)
            .IsRequired()
            .HasColumnName("CreatedByIp");

        builder
            .HasOne(p => p.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(p => p.UserId);

        builder
            .Property(p => p.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder
            .Property(p => p.RevokedDate)
            .IsRequired(false)
            .HasColumnType(ConfigurationSettings.TimeStampWithTimeZone)
            .HasColumnName("RevokedDate");

        builder
            .Property(p => p.RevokedByIp)
            .IsRequired(false)
            .HasColumnName("RevokedByIp");

        builder
            .Property(p => p.ReplacedByToken)
            .IsRequired(false)
            .HasColumnName("ReplacedByToken");

        builder
            .Property(p => p.ReasonRevoked)
            .IsRequired(false)
            .HasColumnName("ReasonRevoked");

        builder.Ignore(p => p.IsExpired);
        builder.Ignore(p => p.IsRevoked);
        builder.Ignore(p => p.IsActive);
    }
}