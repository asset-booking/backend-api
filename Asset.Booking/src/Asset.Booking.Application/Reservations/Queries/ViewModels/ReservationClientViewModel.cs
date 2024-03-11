namespace Asset.Booking.Application.Reservations.Queries.ViewModels;
using System;

public record ReservationClientViewModel(
    Guid ClientId,
    string CompanyName,
    string CompanyPhoneNumber,
    string Email,
    string CoordinatorPhoneNumber);