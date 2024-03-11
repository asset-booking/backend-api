namespace Asset.Booking.Application.Clients.Queries;
using System.Collections.Generic;
using Asset.Booking.Application.Abstractions.Messaging;
using Asset.Booking.Application.Clients.Queries.ViewModels;

public record GetClientsByCompanyNameQuery(string CompanyName)
    : IQuery<IEnumerable<ClientViewModel>>;
