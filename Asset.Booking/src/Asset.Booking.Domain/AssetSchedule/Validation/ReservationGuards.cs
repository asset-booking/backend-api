namespace Asset.Booking.Domain.AssetSchedule.Validation;
using System;
using Ardalis.GuardClauses;
using SharedKernel;
using SharedKernel.Exceptions;

public static class ReservationGuards
{
    public static DateRange ChangeAfterReservationStarted(this IGuardClause guardClause, DateRange reservationInterval)
    {
        if (reservationInterval.StartDate < DateTime.Now)
        {
            throw new AssetBookingException(BookingErrors.Reservations.ReservationUpdateAfterStart);
        }

        return reservationInterval;
    }

    public static DateRange InvalidInterval(this IGuardClause guardClause, DateRange reservationInterval)
    {
        if (reservationInterval.IntervalNights < 1)
        {
            throw new AssetBookingException(BookingErrors.Reservations.InvalidInterval);
        }

        return reservationInterval;
    }

    public static void ReservationOverlapping(
        this IGuardClause guardClause,
        IEnumerable<Reservation> reservations,
        DateRange reservationInterval,
        Guid? existingReservationId = null)
    {
        if (reservations.Any(r => !r.Id.Equals(existingReservationId)
                                  && r.Interval.Overlaps(reservationInterval)))
        {
            throw new AssetBookingException(BookingErrors.Reservations.IntervalOverlaps);
        }
    }
}