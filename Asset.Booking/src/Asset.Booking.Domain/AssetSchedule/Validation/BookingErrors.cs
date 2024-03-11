namespace Asset.Booking.Domain.AssetSchedule.Validation;
using SharedKernel;

public static class BookingErrors
{
    public static class Reservations
    {
        public static readonly Error InvalidReservation =
            new("Reservations.InvalidState", "Some of the reservation properties are wrong.");

        public static readonly Error Duplicate =
            new("Reservations.Duplicate", "This reservation already exists.");

        public static readonly Error TotalCostMiscalculation =
            new("Reservations.Total", "The passed reservation total cost is not the same as the expected one.");

        public static readonly Error VatCostMiscalculation =
            new("Reservations.VAT", "The passed reservation VAT cost is not the same as the expected one.");

        public static readonly Error InvalidInterval =
            new("Reservations.Interval", "A reservation must have more than 1 night.");

        public static readonly Error IntervalOverlaps =
            new("Reservations.Overlap", "This reservation interval overlaps an already existing one.");

        public static readonly Error InvalidStatus =
            new("Reservations.Status", "Passed status is not valid for reservations.");

        public static readonly Error ReservationUpdateAfterStart =
            new("Reservations.UpdateAfterStart", "Cannot change this reservation as it already started.");

        public static readonly Error InvalidCostParameters =
            new("Reservations.Costs", "The reservation cost could not be calculated, either because of the used price, interval or number of people.");
    }
}
