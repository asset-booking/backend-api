namespace Asset.Booking.Application.Clients.Commands;
using Abstractions.Messaging;
using Asset.Booking.Application.Clients.Commands.Dto;

public record RegisterClientCommand(Guid ClientId, RegisterClientDto RegisterClientDto) : ICommand;
