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

        builder.Property(p => p.Id)
            .HasConversion(id => id.ToString(),
                value => RefreshTokenId.Parse(value));

        builder.OwnsOne(
            x => x.Token, sa =>
            {
                sa.Property(p => p.Value)
                    .HasColumnName("Token");
            });

        builder.OwnsOne(
            x => x.Expiration, sa =>
            {
                sa.Property(p => p.Value)
                    .HasColumnName("Expiration");
            });

        builder.OwnsOne(
            x => x.CreationDate, sa =>
            {
                sa.Property(p => p.Value)
                    .HasColumnName("CreationDate");
            });

        builder.OwnsOne(
            x => x.CreatedByIp, sa =>
            {
                sa.Property(p => p.Value)
                    .HasColumnName("CreatedByIp");
            });

        builder.HasOne(p => p.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(p => p.UserId);

        builder.Property(p => p.UserId)
            .HasConversion(id => id.ToString(),
                            value => UserId.Parse(value));
        
        builder.OwnsOne(
            x => x.Revoked, sa =>
            {
                sa.Property(p => p.Value)
                    .IsRequired(false)
                    .HasColumnName("Revoked");
            });
        
        builder.OwnsOne(
            x => x.RevokedByIp, sa =>
            {
                sa.Property(p => p.Value)
                    .IsRequired(false)
                    .HasColumnName("RevokedByIp");
            });
        
        builder.OwnsOne(
            x => x.ReplacedByToken, sa =>
            {
                sa.Property(p => p.Value)
                    .IsRequired(false)
                    .HasColumnName("ReplacedByToken");
            });
        
        builder.OwnsOne(
            x => x.ReasonRevoked, sa =>
            {
                sa.Property(p => p.Value)
                    .IsRequired(false)
                    .HasMaxLength(ConfigurationSettings.MaxStringLength)
                    .HasColumnName("ReasonRevoked");
            });

        builder.Ignore(p => p.IsExpired);
        builder.Ignore(p => p.IsRevoked);
        builder.Ignore(p => p.IsActive);
    }
}