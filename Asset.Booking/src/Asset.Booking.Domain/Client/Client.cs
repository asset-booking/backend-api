namespace Asset.Booking.Domain.Client;

using Ardalis.GuardClauses;
using SharedKernel;
using SharedKernel.Abstractions;
using Validation;

public class Client : Entity<Guid>, IAggregateRoot
{
    private Client(
        Guid id,
        string companyName)
    {
        Id = Guard.Against.Default(id);
        CompanyName = Guard.Against.MissingCompanyName(companyName);

        Contacts = default!;
    }
    
    public Client(
        Guid id,
        string companyName,
        Contacts contacts,
        Address? address = null) : this(id, companyName)
    {
        Contacts = contacts;
        Address = address;
    }

    public Contacts Contacts { get; }

    public Address? Address { get; }

    public string CompanyName { get; }
}
