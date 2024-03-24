namespace Asset.Booking.API.Dto;

public record UpdateReservationIntervalDto(
    Guid ReservationId,
    DateTime OldStart,
    DateTime OldEnd,
    DateTime NewStart,
    DateTime NewEnd);