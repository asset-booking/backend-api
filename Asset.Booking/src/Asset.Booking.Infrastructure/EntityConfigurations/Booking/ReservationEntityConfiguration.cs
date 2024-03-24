namespace Asset.Booking.Infrastructure.EntityConfigurations.Booking;

using Domain.AssetSchedule;
using Domain.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel;

public class ReservationEntityConfiguration: IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations");
        
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(r => r.ModeratorId)
            .HasColumnName("moderator_id");

        builder.Property(r => r.ScheduleId)
            .HasColumnName("schedule_id");
        
        builder.Property(r => r.ClientId)
            .HasColumnName("client_id");
        builder
            .HasOne<Client>()
            .WithMany()
            .HasForeignKey(r => r.ClientId)
            .IsRequired();
        
        builder
            .Property(r => r.Status)
            .HasColumnName("status")
            .HasConversion(
                s => s.Name,
                statusName => Enumeration.FromName<Status>(statusName)!);
        
        builder.OwnsOne(
            r => r.Interval,
            ri =>
            {
                ri.Property(i => i.StartDate)
                    .HasColumnName("interval_start")
                    .HasColumnType("Date");
                ri.Property(i => i.EndDate)
                    .HasColumnName("interval_end")
                    .HasColumnType("Date");
                ri.Ignore(i => i.IntervalNights);
            });
        
         builder.OwnsOne(r => r.Cost, rc =>
         {
             rc.Property(c => c.PricePerPerson).HasColumnName("price_per_person");
             rc.Property(c => c.ServiceFee).HasColumnName("service_fee");
             rc.Property(c => c.NumberOfPeople).HasColumnName("nr_of_people");
             rc.Property(c => c.NumberOfNights).HasColumnName("nr_of_nights");
             rc.Property(c => c.VatPercentage).HasColumnName("vat_percent");

             rc.Property(c => c.VatCost).HasColumnName("vat_cost");
             rc.Property(c => c.TotalCost).HasColumnName("total_cost");
         });
    }
}