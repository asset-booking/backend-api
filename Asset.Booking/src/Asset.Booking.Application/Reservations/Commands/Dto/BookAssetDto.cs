namespace Asset.Booking.Application.Reservations.Commands.Dto;

public record BookAssetDto(
    IEnumerable<ScheduleReservationIdsDto> ScheduleReservationIds,
    int StatusId,
    Guid ClientId,
    DateTime StartDate,
    DateTime EndDate,
    decimal PricePerPerson,
    int NumberOfPeople,
    decimal ServiceFee,
    float VatPercentage);

public record ScheduleReservationIdsDto(
    int ScheduleId,
    Guid ReservationId);