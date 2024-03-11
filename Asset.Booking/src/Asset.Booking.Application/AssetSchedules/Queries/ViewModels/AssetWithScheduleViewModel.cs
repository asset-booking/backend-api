namespace Asset.Booking.Application.AssetSchedules.Queries.ViewModels;

public record AssetWithScheduleViewModel(
    int AssetId,
    int ScheduleId,
    IEnumerable<ScheduleReservationViewModel> Reservations);