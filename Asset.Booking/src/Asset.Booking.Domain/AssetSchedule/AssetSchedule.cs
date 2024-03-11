namespace Asset.Booking.Domain.AssetSchedule;

using Ardalis.GuardClauses;
using SharedKernel;
using SharedKernel.Abstractions;
using SharedKernel.Exceptions;
using Validation;

public class AssetSchedule(int assetId) : Entity<int>, IAggregateRoot
{
    private readonly List<Reservation> _reservations = new();

    public void BookReservation(
        Guid reservationId,
        Guid clientId,
        int moderatorId,
        Status status,
        DateRange interval,
        Cost cost)
    {
        Guard.Against.ReservationOverlapping(_reservations, interval);

        var reservation = new Reservation(
            reservationId,
            moderatorId,
            clientId,
            AssetId,
            status,
            interval,
            cost);

        _reservations.Add(reservation);
    }

    public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();

    public int AssetId { get; } = Guard.Against.Default(assetId);

    public void RescheduleReservation(Guid reservationId, DateRange newInterval)
    {
        Guard.Against.Default(reservationId, nameof(reservationId));
        Guard.Against.InvalidInterval(newInterval);
        Guard.Against.ReservationOverlapping(_reservations, newInterval);

        var reservation = GetReservation(reservationId);
        if (reservation is null)
        {
            ThrowReservationNotFound(reservationId);
            return;
        }

        reservation.ChangeInterval(newInterval);
    }

    public void RemoveReservation(Guid reservationId)
    {
        Guard.Against.Default(reservationId, nameof(reservationId));

        var reservation = GetReservation(reservationId);
        if (reservation is null)
        {
            ThrowReservationNotFound(reservationId);
            return;
        }

        Guard.Against.ChangeAfterReservationStarted(reservation.Interval);
        _reservations.Remove(reservation);
    }

    public Reservation? GetReservation(Guid reservationId) =>
        _reservations.Find(r => r.Id.Equals(reservationId));

    private void ThrowReservationNotFound(Guid reservationId) =>
        throw new AssetBookingException(GenericErrors.EntityNotFound(
            nameof(Reservation),
            nameof(reservationId),
            reservationId.ToString()));
}
