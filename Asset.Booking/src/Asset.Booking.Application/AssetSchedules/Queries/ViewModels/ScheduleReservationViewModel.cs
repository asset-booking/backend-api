namespace Asset.Booking.Application.AssetSchedules.Queries.ViewModels;
using System;

public record ScheduleReservationViewModel(
    Guid Id,
    int StatusId,
    DateTime Start,
    DateTime End,
    decimal Total,
    string CoordinatorPhoneNumber,
    string ModeratorName);