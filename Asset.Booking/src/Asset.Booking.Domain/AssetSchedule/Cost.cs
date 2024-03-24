namespace Asset.Booking.Domain.AssetSchedule;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using SharedKernel;
using SharedKernel.Exceptions;
using Validation;

public class Cost : ValueObject
{
    /// <summary>
    /// Creates a cost instance.
    /// </summary>
    /// <exception cref="ArgumentException">For invalid parameters.</exception>
    public Cost(
        decimal pricePerPerson,
        decimal serviceFee,
        int numberOfPeople,
        int numberOfNights,
        float vatPercentage)
    {
        try
        {
            PricePerPerson = Guard.Against.Negative(pricePerPerson, nameof(pricePerPerson));
            ServiceFee = Guard.Against.Negative(serviceFee, nameof(serviceFee));
            NumberOfPeople = Guard.Against.NegativeOrZero(numberOfPeople, nameof(numberOfPeople));
            NumberOfNights = Guard.Against.NegativeOrZero(numberOfNights, nameof(numberOfNights));
            VatPercentage = Guard.Against.Negative(vatPercentage, nameof(vatPercentage));
        }
        catch
        {
            throw new AssetBookingException(BookingErrors.Reservations.InvalidCostParameters);
        }
        
        var subtotalWithoutVat = PricePerPerson * NumberOfNights * NumberOfPeople;
        VatCost = (subtotalWithoutVat + ServiceFee) * (decimal)VatPercentage / 100;
        TotalCost = subtotalWithoutVat + ServiceFee + VatCost;
    }

    public decimal PricePerPerson { get; }
    public decimal ServiceFee { get; }
    public int NumberOfPeople { get; }
    public int NumberOfNights { get; }
    public float VatPercentage { get; }

    public decimal VatCost { get; }
    public decimal TotalCost { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PricePerPerson;
        yield return ServiceFee;
        yield return NumberOfPeople;
        yield return NumberOfNights;
        yield return VatPercentage;
    }
}
