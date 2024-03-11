namespace Asset.Booking.Application.Reservations.Commands;

using Abstractions.Messaging;
using SharedKernel;

public record RescheduleCommand(
    Guid ReservationId,
    DateRange NewInterval) : ICommand;