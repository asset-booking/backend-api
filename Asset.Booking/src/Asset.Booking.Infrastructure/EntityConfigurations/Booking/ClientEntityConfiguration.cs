namespace Asset.Booking.Infrastructure.EntityConfigurations.Booking;

using Domain.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel;

public class ClientEntityConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(c => c.CompanyName)
            .HasColumnName("company_name");

        builder.OwnsOne<Address>(c => c.Address, adr =>
        {
            adr.Property(a => a.City).HasColumnName("adr_city");
            adr.Property(a => a.ZipCode).HasColumnName("adr_zip");
            adr.Property(a => a.Street).HasColumnName("adr_street");
            adr.Property(a => a.StreetNumber).HasColumnName("adr_street_nr");
        });

        builder.OwnsOne<Contacts>(c => c.Contacts, contact =>
        {
            contact.Property(c => c.Email)
                .HasColumnName("email");
            
            contact.OwnsMany(c => c.PhoneNumbers, cp =>
            {
                cp.ToTable("phone_numbers");
                cp.Property<int>("id")
                    .ValueGeneratedOnAdd();
                cp.HasKey("id");
                
                cp.WithOwner()
                    .HasForeignKey("client_id");
                
                cp.Property(p => p.Number)
                    .HasColumnName("number");
                
                cp.Property(pt => pt.Type)
                    .HasColumnName("type")
                    .HasConversion(
                        t => t.Name,
                        typeName => Enumeration.FromName<PhoneNumberType>(typeName)!);
            });
        });
    }
}