namespace Asset.Booking.Domain.Client;

using Ardalis.GuardClauses;
using SharedKernel;
using Validation;

public class Contacts : ValueObject
{
    private Contacts(string email) =>
        Email = Guard.Against.InvalidEmailAddress(email);

    public Contacts(string email,
        IEnumerable<PhoneNumber>? phoneNumbers) : this(email) => 
        PhoneNumbers = phoneNumbers?.ToList().AsReadOnly()
            ?? new List<PhoneNumber>().AsReadOnly();
    
    public string Email { get; }
    public IReadOnlyCollection<PhoneNumber> PhoneNumbers { get; } = [];

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email.ToLowerInvariant();
        foreach (PhoneNumber phoneNumber in PhoneNumbers)
        {
            yield return phoneNumber.Type.Id;
            yield return phoneNumber.Number.ToLowerInvariant().Replace(" ", string.Empty);
        }
    }
}
