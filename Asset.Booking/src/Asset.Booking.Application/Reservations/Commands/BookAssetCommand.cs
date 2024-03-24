namespace Asset.Booking.Application.Reservations.Commands;

using Abstractions.Messaging;
using Asset.Booking.Application.Reservations.Commands.Dto;

public record BookAssetCommand(BookAssetDto BookAssetDto) : ICommand;
