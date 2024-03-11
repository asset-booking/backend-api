namespace Asset.Booking.Domain.Client;

using Ardalis.GuardClauses;
using SharedKernel;
using Validation;

public class Contacts(
        string email,
        IEnumerable<PhoneNumber>? phoneNumbers = null) : ValueObject
{
    public string Email { get; } = Guard.Against.InvalidEmailAddress(email);
    public IReadOnlyCollection<PhoneNumber> PhoneNumbers { get; } = phoneNumbers?.ToList().AsReadOnly()
                                                                    ?? new List<PhoneNumber>().AsReadOnly();

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
