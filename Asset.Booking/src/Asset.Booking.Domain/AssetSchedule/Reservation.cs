namespace Asset.Booking.Domain.AssetSchedule;

using Ardalis.GuardClauses;
using SharedKernel;
using SharedKernel.Exceptions;
using Validation;

public class Reservation : Entity<Guid>
{
    public Reservation(
        Guid id,
        int moderatorId,
        Guid clientId,
        int assetId,
        Status status,
        DateRange interval,
        Cost cost)
    {
        Id = Guard.Against.Default(id, nameof(id));
        AssetId = Guard.Against.Default(assetId, nameof(assetId));
        ModeratorId = Guard.Against.Default(moderatorId, nameof(moderatorId));
        ClientId = Guard.Against.Default(clientId, nameof(clientId));
        Interval = Guard.Against.InvalidInterval(interval);
        Status = status;
        Cost = cost;
    }

    public int AssetId { get; }
    public int ModeratorId { get; }
    public Guid ClientId { get; }
    public Status Status { get; private set; }
    public DateRange Interval { get; private set; }
    public Cost Cost { get; private set; }

    public void ChangePricePerPerson(decimal pricePerPerson)
    {
        Guard.Against.ChangeAfterReservationStarted(Interval);
        
        Cost = new Cost(
            pricePerPerson,
            Cost.ServiceFee,
            Cost.NumberOfPeople,
            Interval.IntervalNights,
            Cost.VatPercentage);
    }

    public void ChangeStatus(int statusId)
    {
        var status = Enumeration.FromValue<Status>(statusId);
        Status = status ?? throw new AssetBookingException(BookingErrors.Reservations.InvalidStatus);
    }

    internal void ChangeInterval(DateRange interval)
    {
        if (IsStartDateChanged(interval)) Guard.Against.ChangeAfterReservationStarted(Interval);
        Guard.Against.InvalidInterval(interval);

        var cost = new Cost(
            Cost.PricePerPerson,
            Cost.ServiceFee,
            Cost.NumberOfPeople,
            interval.IntervalNights,
            Cost.VatPercentage);

        Interval = interval;
        Cost = cost;
    }

    private bool IsStartDateChanged(DateRange interval) =>
        !(interval.StartDate == Interval.StartDate);
}
