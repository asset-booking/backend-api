namespace Asset.Booking.Application.Reservations.Queries.ViewModels;
using System;

public record ReservationViewModel(
    Guid ReservationId,
    int AssetId,
    DateTime StartDate,
    DateTime EndDate,
    int StatusId,
    ReservationClientViewModel ReservationClient,
    CostsViewModel Cost);