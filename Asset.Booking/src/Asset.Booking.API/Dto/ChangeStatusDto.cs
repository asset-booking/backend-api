namespace Asset.Booking.API.Dto;

public record ChangeStatusDto(Guid ReservationId, int NewStatusId);