namespace Asset.Booking.Application.Clients.Queries.ViewModels;
using System;

public record ClientViewModel(
    Guid ClientId,
    string CompanyName,
    string Email,
    string CompanyPhoneNumber,
    string CoordinatorPhoneNumber);
