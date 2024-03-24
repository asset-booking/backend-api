namespace Asset.Booking.Application.Reservations.Commands;

using Abstractions.Messaging;

public record CancelReservationCommand(Guid ReservationId): ICommand;