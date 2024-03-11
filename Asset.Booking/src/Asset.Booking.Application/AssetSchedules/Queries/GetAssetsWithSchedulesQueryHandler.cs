namespace Asset.Booking.Application.AssetSchedules.Queries;
using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.Application.AssetSchedules.Queries.ViewModels;
using Infrastructure;
using Shared;
using SharedKernel;

public class GetAssetsWithSchedulesQueryHandler : IQueryHandler<GetAssetsWithSchedulesQuery, IEnumerable<AssetWithScheduleViewModel>>
{
    public Task<Result<IEnumerable<AssetWithScheduleViewModel>>> Handle(GetAssetsWithSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        fakedb.IncludeAssetReservations(request.Interval);
        var result = new List<AssetWithScheduleViewModel>();

        foreach (var asset in fakedb.assets)
        {
            var assetSchedule = fakedb.assetSchedules.Find(a => a.AssetId.Equals(asset.Id));
            if (assetSchedule is not null)
            {
                result.Add(new AssetWithScheduleViewModel(
                    assetSchedule.AssetId,
                    assetSchedule.Id,
                    assetSchedule.Reservations.Select(r => new ScheduleReservationViewModel(
                        r.Id,
                        r.Status.Id,
                        r.Interval.StartDate,
                        r.Interval.EndDate,
                        r.Cost.TotalCost,
                        r.GetCoordinatorPhone(),
                        r.GetModeratorName()))));
            }
        }

        return Task.FromResult(Result<IEnumerable<AssetWithScheduleViewModel>>.Success(result.AsEnumerable()));
    }
}
