namespace Asset.Booking.Application.Reservations.Queries.ViewModels;
using System;

public record ReservationViewModel(
    Guid Id,
    int AssetId,
    int ScheduleId,
    DateTime StartDate,
    DateTime EndDate,
    int StatusId,
    string ModeratorName,
    ReservationClientViewModel Client,
    CostsViewModel Cost);