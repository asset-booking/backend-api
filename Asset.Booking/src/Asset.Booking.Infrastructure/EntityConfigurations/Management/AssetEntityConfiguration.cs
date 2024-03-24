namespace Asset.Booking.Infrastructure.EntityConfigurations.Management;

using Asset.Management.Domain.Asset;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValueComparers;

public class AssetEntityConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("assets");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasColumnName("id");
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(a => a.CategoryId);

        builder.Property(a => a.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();
        
        builder.Property(a => a.Specification)
            .HasColumnName("specification")
            .IsRequired();
        
        builder.Property(a => a.Note)
            .HasColumnName("note")
            .IsRequired(false);
        
        builder.Property(a => a.SpecificationIcons)
            .HasColumnName("specification_icons")
            .HasConversion(
                s => string.Join(";", s),
                s => s.Split(";", StringSplitOptions.RemoveEmptyEntries),
                new StringCollectionValueComparer())
            .HasMaxLength(500)
            .IsRequired(false);
        
        builder.Property(a => a.NoteIcons)
            .HasColumnName("note_icons")
            .HasConversion(
                n => string.Join(";", n),
                n => n.Split(";", StringSplitOptions.RemoveEmptyEntries),
                new StringCollectionValueComparer())
            .HasMaxLength(500)
            .IsRequired(false);
    }
}