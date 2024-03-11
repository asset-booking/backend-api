namespace Asset.Booking.Application.Reservations.Commands;
using System;
using Abstractions.Messaging;

public record ChangePriceCommand(
    Guid ReservationId,
    decimal NewPricePerPerson) : ICommand;
