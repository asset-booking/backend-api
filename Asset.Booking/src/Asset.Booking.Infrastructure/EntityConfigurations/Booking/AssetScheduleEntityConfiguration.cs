namespace Asset.Booking.Infrastructure.EntityConfigurations.Booking;

using Domain.AssetSchedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AssetScheduleEntityConfiguration: IEntityTypeConfiguration<AssetSchedule>
{
    public void Configure(EntityTypeBuilder<AssetSchedule> builder)
    {
        builder.ToTable("asset_schedules");
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.AssetId).HasColumnName("asset_id");

        builder
            .HasMany(a => a.Reservations)
            .WithOne()
            .HasForeignKey(r => r.ScheduleId)
            .IsRequired();
    }
}