using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildForest.Domain.Languages.Entities;
using WildForest.Domain.Languages.ValueObjects;

namespace WildForest.Infrastructure.Persistence.Configurations;

public sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => LanguageId.Create(value));

        builder
            .Property(p => p.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(ConfigurationSettings.MaxStringLength);

        builder.Metadata
            .FindNavigation(nameof(Language.Users))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}