namespace Asset.Booking.Domain.Client;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using SharedKernel;
using Validation;

public class PhoneNumber(string number, PhoneNumberType type) : ValueObject
{
    public string Number { get; } = Guard.Against.PhoneNumberContainsNotAllowedCharacters(number.Trim());
    public PhoneNumberType Type { get; } = type;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type.Id;
        yield return Number.ToLowerInvariant().Replace(" ", string.Empty);
    }

    public override string ToString() => Number;
}
