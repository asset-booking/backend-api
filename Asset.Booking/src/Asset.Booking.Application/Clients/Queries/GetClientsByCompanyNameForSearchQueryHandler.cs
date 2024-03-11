namespace Asset.Booking.Application.Clients.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.Domain.Client;
using Infrastructure;
using SharedKernel;
using ViewModels;

public class GetClientsByCompanyNameForSearchQueryHandler : IQueryHandler<GetClientsByCompanyNameForSearchQuery, IEnumerable<ClientSearchResultViewModel>>
{
    public Task<Result<IEnumerable<ClientSearchResultViewModel>>> Handle(GetClientsByCompanyNameForSearchQuery request,
        CancellationToken cancellationToken)
    {
        var clients = fakedb.clients.Where(c => c.CompanyName.StartsWith(request.CompanyName, StringComparison.OrdinalIgnoreCase));
        var result = new List<ClientSearchResultViewModel>();

        foreach (Client client in clients)
        {
            var viewModels = fakedb.reservations
                .Where(r => r.ClientId.Equals(client.Id) && r.Interval.StartDate >= DateTime.Now)
                .Select(cr => new ClientSearchResultViewModel(
                    client.Id,
                    client.CompanyName,
                    cr.Interval.StartDate,
                    cr.Interval.EndDate,
                    GetPhoneNumber(client, PhoneNumberType.Company),
                    client.Contacts.Email));

            result.AddRange(viewModels);
        }

        return Task.FromResult(Result<IEnumerable<ClientSearchResultViewModel>>.Success(result));
    }

    private string GetPhoneNumber(Client client, PhoneNumberType type) =>
        client.Contacts.PhoneNumbers.FirstOrDefault(p => p.Type.Equals(type))?.ToString() ??
        string.Empty;
}
