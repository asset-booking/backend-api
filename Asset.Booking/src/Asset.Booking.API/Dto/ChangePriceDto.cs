namespace Asset.Booking.API.Dto;

public record ChangePriceDto(Guid ReservationId, decimal NewPrice);