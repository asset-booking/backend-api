namespace Asset.Booking.Application.AssetSchedules.Queries;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.Infrastructure;
using Shared;
using SharedKernel;
using ViewModels;

public class GetAssetScheduleByAssetIdQueryHandler : IQueryHandler<GetAssetScheduleByAssetIdQuery, AssetWithScheduleViewModel>
{
    public Task<Result<AssetWithScheduleViewModel>> Handle(GetAssetScheduleByAssetIdQuery request,
        CancellationToken cancellationToken)
    {
        fakedb.IncludeAssetReservations(request.Interval);
        var assetSchedule = fakedb.assetSchedules.Find(a => a.AssetId.Equals(request.AssetId));
        if (assetSchedule is null)
        {
            return Task.FromResult(
                Result<AssetWithScheduleViewModel>.Failure(GenericErrors.EntityNotFound(nameof(assetSchedule))));
        }

        var scheduleViewModel = new AssetWithScheduleViewModel(
            assetSchedule.AssetId,
            assetSchedule.Id,
            assetSchedule.Reservations.Select(r => new ScheduleReservationViewModel(
                r.Id,
                r.Status.Id,
                r.Interval.StartDate,
                r.Interval.EndDate,
                r.Cost.TotalCost,
                r.GetCoordinatorPhone(),
                r.GetModeratorName())));

        return Task.FromResult(Result<AssetWithScheduleViewModel>.Success(scheduleViewModel));
    }
}
