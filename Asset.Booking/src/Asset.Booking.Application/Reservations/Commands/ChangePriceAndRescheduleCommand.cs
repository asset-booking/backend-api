namespace Asset.Booking.Application.Reservations.Commands;
using System;
using Abstractions.Messaging;
using Asset.Booking.SharedKernel;

public record ChangePriceAndRescheduleCommand(
    Guid ReservationId,
    decimal NewPricePerPerson,
    DateRange NewInterval) : ICommand;
