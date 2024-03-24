namespace Asset.Booking.Application.Reservations.Queries;

using Abstractions.Messaging;
using Dto;
using ViewModels;

public record GetReservationCostQuery(CostDto Cost) : IQuery<CostsViewModel>;