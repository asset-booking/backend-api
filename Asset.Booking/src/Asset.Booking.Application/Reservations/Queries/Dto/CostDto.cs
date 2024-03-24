namespace Asset.Booking.Application.Reservations.Queries.Dto;

public record CostDto(
    decimal PricePerPerson,
    int NumberOfPeople,
    int NumberOfNights,
    decimal ServiceFee,
    float VatPercentage);