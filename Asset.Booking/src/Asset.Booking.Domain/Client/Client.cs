namespace Asset.Booking.Domain.Client;

using Ardalis.GuardClauses;
using SharedKernel;
using SharedKernel.Abstractions;
using Validation;

public class Client : Entity<Guid>, IAggregateRoot
{
    public Client(
        Guid id,
        string companyName,
        Contacts contacts,
        Address? address = null)
    {
        Id = Guard.Against.Default(id);
        CompanyName = Guard.Against.MissingCompanyName(companyName);
        Contacts = contacts;
        Address = address;
    }

    public Contacts Contacts { get; }

    public Address? Address { get; }

    public string CompanyName { get; }
}
