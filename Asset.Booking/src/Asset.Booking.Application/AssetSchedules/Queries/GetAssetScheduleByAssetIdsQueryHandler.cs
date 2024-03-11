namespace Asset.Booking.Application.AssetSchedules.Queries;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.Infrastructure;
using Shared;
using SharedKernel;
using ViewModels;

public class GetAssetScheduleByAssetIdsQueryHandler
    : IQueryHandler<GetAssetScheduleByAssetIdsQuery, IEnumerable<AssetWithScheduleViewModel>>
{
    public Task<Result<IEnumerable<AssetWithScheduleViewModel>>> Handle(GetAssetScheduleByAssetIdsQuery request,
        CancellationToken cancellationToken)
    {
        fakedb.IncludeAssetReservations(request.Interval);
        var assetSchedules = fakedb.assetSchedules
            .Where(a => request.AssetIds.Contains(a.AssetId));

        var result = new List<AssetWithScheduleViewModel>();
        foreach (var schedule in assetSchedules)
        {
            result.Add(new AssetWithScheduleViewModel(
                schedule.AssetId,
                schedule.Id,
                schedule.Reservations.Select(r => new ScheduleReservationViewModel(
                    r.Id,
                    r.Status.Id,
                    r.Interval.StartDate,
                    r.Interval.EndDate,
                    r.Cost.TotalCost,
                    r.GetCoordinatorPhone(),
                    r.GetModeratorName()))));
        }

        return Task.FromResult(Result<IEnumerable<AssetWithScheduleViewModel>>.Success(result));
    }
}
