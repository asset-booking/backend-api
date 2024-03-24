namespace Asset.Booking.Application.Reservations.Queries.ViewModels;

public record CostsViewModel(
    decimal PricePerPerson,
    decimal ServiceFee,
    int NumberOfNights,
    float VatPercentage,
    decimal VatCost,
    decimal TotalCost);