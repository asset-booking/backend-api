namespace Asset.Booking.Infrastructure.EntityConfigurations.Management;

using Asset.Management.Domain.Asset;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryEntityConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        builder.Property(a => a.Id).HasColumnName("id");
        builder.HasKey(a => a.Id);

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(c => c.ParentCategoryId)
            .HasColumnName("parent_category_id")
            .IsRequired(false);
        
        builder.HasMany(c => c.SubCategories)
            .WithOne()
            .HasForeignKey(c => c.ParentCategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}