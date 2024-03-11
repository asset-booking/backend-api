namespace Asset.Booking.Application.Reservations.Queries;
using System;
using Abstractions.Messaging;
using ViewModels;

public record GetReservationByIdQuery(Guid Id) : IQuery<ReservationViewModel>;