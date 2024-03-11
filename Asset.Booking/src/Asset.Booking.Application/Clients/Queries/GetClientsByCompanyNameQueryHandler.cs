using Asset.Booking.Application.Clients.Queries.ViewModels;

namespace Asset.Booking.Application.Clients.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.Domain.Client;
using Asset.Booking.Infrastructure;
using SharedKernel;

public class GetClientsByCompanyNameQueryHandler : IQueryHandler<GetClientsByCompanyNameQuery, IEnumerable<ClientViewModel>>
{
    public Task<Result<IEnumerable<ClientViewModel>>> Handle(GetClientsByCompanyNameQuery request, CancellationToken cancellationToken)
    {
        var clients = fakedb.clients.Where(c => c.CompanyName.StartsWith(request.CompanyName, StringComparison.OrdinalIgnoreCase));
        var result = clients.Select(c => new ClientViewModel(
            c.Id,
            c.CompanyName,
            c.Contacts.Email,
            GetPhoneNumber(c, PhoneNumberType.Company),
            GetPhoneNumber(c, PhoneNumberType.Coordinator)));


        return Task.FromResult(Result<IEnumerable<ClientViewModel>>.Success(result));
    }

    private string GetPhoneNumber(Client client, PhoneNumberType type) =>
        client.Contacts.PhoneNumbers.FirstOrDefault(p => p.Type.Equals(type))?.ToString() ??
        string.Empty;
}
