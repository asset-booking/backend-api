namespace Asset.Booking.Application.Reservations.Commands.Dto;

public record BookAssetDto(
    int ScheduleId,
    int StatusId,
    Guid ClientId,
    DateTime StartDate,
    DateTime EndDate,
    decimal PricePerPerson,
    int NumberOfPeople,
    decimal ServiceFee,
    float VatPercentage);