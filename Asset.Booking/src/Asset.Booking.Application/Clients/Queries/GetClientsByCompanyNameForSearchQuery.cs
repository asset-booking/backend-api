namespace Asset.Booking.Application.Clients.Queries;
using System.Collections.Generic;
using Abstractions.Messaging;
using ViewModels;

public record GetClientsByCompanyNameForSearchQuery(string CompanyName)
    : IQuery<IEnumerable<ClientSearchResultViewModel>>;
