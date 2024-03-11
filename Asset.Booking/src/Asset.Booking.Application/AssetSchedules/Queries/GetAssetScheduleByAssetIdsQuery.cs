namespace Asset.Booking.Application.AssetSchedules.Queries;
using Abstractions.Messaging;
using SharedKernel;
using ViewModels;

public record GetAssetScheduleByAssetIdsQuery(IEnumerable<int> AssetIds, DateRange Interval)
    : IQuery<IEnumerable<AssetWithScheduleViewModel>>;
