namespace Asset.Booking.Application.Reservations.Commands;
using System;
using Abstractions.Messaging;

public record ChangeStatusCommand(
    Guid ReservationId,
    int NewStatusId) : ICommand;
