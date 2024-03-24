namespace Asset.Booking.Application.AssetSchedules.Queries;

using Abstractions.Configurations;
using Abstractions.Messaging;
using Dapper;
using Domain.AssetSchedule;
using Npgsql;
using SharedKernel;
using ViewModels;

public class GetAssetScheduleByAssetIdsQueryHandler(IApplicationConfiguration configuration)
    : IQueryHandler<GetAssetScheduleByAssetIdsQuery, IEnumerable<AssetWithScheduleViewModel>>
{
    public async Task<Result<IEnumerable<AssetWithScheduleViewModel>>> Handle(GetAssetScheduleByAssetIdsQuery request,
        CancellationToken cancellationToken)
    {
        await using NpgsqlConnection connection = new(configuration.SqlConnectionString);
        var assetsWithSchedulesQuery = @"
            select
                sch.asset_id,
                sch.id as schedule_id,
                r.id reservation_id,
                r.status,
                r.interval_start,
                r.interval_end,
                r.total_cost,
                coordinator.number coordinator_phone
            from asset_schedules sch
            left join reservations r on
                r.schedule_id=sch.id
                and r.status <> 'Cancelled'
                and r.interval_start >= @startDate
                and r.interval_end <= @endDate
            left join lateral (
                select number
                from phone_numbers p
                where p.client_id=r.client_id and type='Coordinator' limit 1) coordinator on true
            where sch.asset_id = any(@assetIds);
        ";
        
        var queryParams = new
        {
            assetIds = request.AssetIds,
            startDate = request.Interval.StartDate,
            endDate = request.Interval.EndDate
        };
        var assetsWithSchedules = await connection.QueryAsync(assetsWithSchedulesQuery, queryParams);
        var grouped = assetsWithSchedules.GroupBy(a => new { a.schedule_id, a.asset_id });
        
        IEnumerable<AssetWithScheduleViewModel> result = grouped
            .Select(gr => new AssetWithScheduleViewModel(
                gr.Key.asset_id,
                gr.Key.schedule_id,
                gr
                    .Where(resGroup => resGroup.reservation_id is not null)
                    .Select(grv => new ScheduleReservationViewModel(
                        grv.reservation_id,
                        Enumeration.FromName<Status>(grv.status).Id,
                        grv.interval_start,
                        grv.interval_end,
                        grv.total_cost,
                        grv.coordinator_phone,
                        "current moderator")
                    )
                )
            );

        return Result<IEnumerable<AssetWithScheduleViewModel>>.Success(result);
    }
}