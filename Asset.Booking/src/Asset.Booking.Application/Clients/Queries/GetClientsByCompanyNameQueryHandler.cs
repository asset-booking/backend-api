namespace Asset.Booking.Application.Clients.Queries;

using System.Collections.Generic;
using ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Configurations;
using Abstractions.Messaging;
using Domain.Client;
using Dapper;
using Npgsql;
using SharedKernel;

public class GetClientsByCompanyNameQueryHandler (IApplicationConfiguration configuration)
    : IQueryHandler<GetClientsByCompanyNameQuery, IEnumerable<ClientViewModel>>
{
    public async Task<Result<IEnumerable<ClientViewModel>>> Handle(GetClientsByCompanyNameQuery request, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(configuration.SqlConnectionString);

        var companyNameParam = new { companyName = string.Concat("%", request.CompanyName, "%") };
        var clientsQuery = @"
            select
                c.id,
                c.company_name,
                c.email,
                coordinator.number coordinator_number,
                comp.number company_number
            from clients c
            left join lateral (
                select number
                from phone_numbers p
                where p.client_id=c.id and type='Coordinator' limit 1) coordinator on true
            left join lateral (
                select number
                from phone_numbers p
                where p.client_id=c.id and type='Company' limit 1) comp on true
            where company_name ilike @companyName
            order by company_name;
        ";

        var clients = await connection.QueryAsync(clientsQuery, companyNameParam);
        var result = clients.Select(c => new ClientViewModel(
            c.id,
            c.company_name,
            c.email,
            c.company_number,
            c.coordinator_number));

        return Result<IEnumerable<ClientViewModel>>.Success(result);
    }
}
