namespace Asset.Booking.Application.AssetSchedules.Queries;
using Abstractions.Messaging;
using Asset.Booking.Application.AssetSchedules.Queries.ViewModels;
using SharedKernel;

public record GetAssetsWithSchedulesQuery(DateRange Interval) : IQuery<IEnumerable<AssetWithScheduleViewModel>>;