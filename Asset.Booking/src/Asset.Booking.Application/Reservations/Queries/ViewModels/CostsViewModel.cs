namespace Asset.Booking.Application.Reservations.Queries.ViewModels;

public record CostsViewModel(
    decimal PricePerPerson,
    decimal ServiceFee,
    float VatPercentage,
    decimal VatCost,
    decimal Total);
