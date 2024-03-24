namespace Asset.Booking.Application.Clients.Queries;

using Abstractions.Configurations;
using Abstractions.Messaging;
using Dapper;
using Npgsql;
using SharedKernel;
using ViewModels;

public class GetClientsByCompanyNameForSearchQueryHandler(IApplicationConfiguration configuration)
    : IQueryHandler<GetClientsByCompanyNameForSearchQuery, IEnumerable<ClientSearchResultViewModel>>
{
    public async Task<Result<IEnumerable<ClientSearchResultViewModel>>> Handle(
        GetClientsByCompanyNameForSearchQuery request,
        CancellationToken cancellationToken)
    {
        await using NpgsqlConnection connection = new NpgsqlConnection(configuration.SqlConnectionString);

        var companyNameParam = new { companyName = request.CompanyName };
        var clientsQuery = @"
            select
                c.id,
                c.company_name,
                r.interval_start,
                r.interval_end,
                c.email,
                company.number company_phone
            from clients c
            join reservations r on r.client_id = c.id
            left join lateral (
                select number
                from phone_numbers p
                where p.client_id=r.client_id and type='Company' limit 1) company on true
            where c.company_name like '@companyName%'
            order by c.company_name, r.interval_start
        ";

        IEnumerable<dynamic> clients = await connection.QueryAsync(clientsQuery, companyNameParam);
        IEnumerable<ClientSearchResultViewModel>? result = clients.Select(r => new ClientSearchResultViewModel(
            r.id,
            r.company_name,
            r.interval_start,
            r.interval_end,
            r.company_phone,
            r.email));

        return Result<IEnumerable<ClientSearchResultViewModel>>.Success(result);
    }
}