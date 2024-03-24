namespace Asset.Booking.Domain.AssetSchedule;

using Ardalis.GuardClauses;
using SharedKernel;
using SharedKernel.Exceptions;
using Validation;

public class Reservation : Entity<Guid>
{
    private Reservation(
        Guid id,
        int scheduleId,
        int moderatorId,
        Guid clientId,
        Status status)
    {
        Id = Guard.Against.Default(id, nameof(id));
        ScheduleId = Guard.Against.Default(scheduleId, nameof(scheduleId));
        ModeratorId = Guard.Against.Default(moderatorId, nameof(moderatorId));
        ClientId = Guard.Against.Default(clientId, nameof(clientId));
        Status = status;

        Interval = default!;
        Cost = default!;
    }
    
    public Reservation(
        Guid id,
        int scheduleId,
        int moderatorId,
        Guid clientId,
        Status status,
        DateRange interval,
        Cost cost): this(id, scheduleId, moderatorId, clientId, status)
    {
        Interval = Guard.Against.InvalidInterval(interval);
        Cost = cost;
    }

    public int ScheduleId { get; }
    public int ModeratorId { get; }
    public Guid ClientId { get; }
    public Status Status { get; private set; }
    public DateRange Interval { get; private set; }
    public Cost Cost { get; private set; }

    public void Cancel()
    {
        Guard.Against.ChangeAfterReservationStarted(Interval);
        Status = Status.Cancelled;
    }
    
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
