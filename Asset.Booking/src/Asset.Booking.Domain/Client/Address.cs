namespace Asset.Booking.Domain.Client;
using SharedKernel;

public class Address(
        string? city = null,
        string? zipCode = null,
        string? street = null,
        string? streetNumber = null) : ValueObject
{
    public string? City { get; } = city;
    public string? ZipCode { get; } = zipCode;
    public string? Street { get; } = street;
    public string? StreetNumber { get; } = streetNumber;

    public bool IsEmpty =>
        string.IsNullOrWhiteSpace(City) &&
        string.IsNullOrWhiteSpace(ZipCode) &&
        string.IsNullOrWhiteSpace(Street) &&
        string.IsNullOrWhiteSpace(StreetNumber);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City?.ToLowerInvariant() ?? string.Empty;
        yield return ZipCode?.ToLowerInvariant() ?? string.Empty;
        yield return Street?.ToLowerInvariant() ?? string.Empty;
        yield return StreetNumber?.ToLowerInvariant() ?? string.Empty;
    }
}