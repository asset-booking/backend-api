namespace Asset.Booking.Application.Clients.Queries.ViewModels;
using System;

public record ClientSearchResultViewModel(
    Guid ClientId,
    string CompanyName,
    DateTime DateIn,
    DateTime DateOut,
    string CompanyPhoneNumber,
    string Email);