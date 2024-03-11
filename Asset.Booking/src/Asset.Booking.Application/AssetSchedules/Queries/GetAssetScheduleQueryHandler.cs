namespace Asset.Booking.Application.AssetSchedules.Queries;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.Infrastructure;
using Shared;
using SharedKernel;
using ViewModels;

public class GetAssetScheduleQueryHandler : IQueryHandler<GetAssetScheduleQuery, AssetWithScheduleViewModel>
{
    public Task<Result<AssetWithScheduleViewModel>> Handle(GetAssetScheduleQuery request,
        CancellationToken cancellationToken)
    {
        fakedb.IncludeAssetReservations(request.Interval);
        var assetSchedule = fakedb.assetSchedules.Find(a => a.Id.Equals(request.ScheduleId));

        if (assetSchedule is null)
        {
            return Task.FromResult(
                Result<AssetWithScheduleViewModel>.Failure(GenericErrors.EntityNotFound(nameof(assetSchedule))));
        }

        var result = new AssetWithScheduleViewModel(
            assetSchedule.AssetId,
            assetSchedule.Id, assetSchedule.Reservations.Select(r => new ScheduleReservationViewModel(
                r.Id,
                r.Status.Id,
                r.Interval.StartDate,
                r.Interval.EndDate,
                r.Cost.TotalCost,
                r.GetCoordinatorPhone(),
                r.GetModeratorName())));
        return Task.FromResult(Result<AssetWithScheduleViewModel>.Success(result));
    }
}
