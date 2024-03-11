namespace Asset.Booking.Application.AssetSchedules.Queries;
using Abstractions.Messaging;
using SharedKernel;
using ViewModels;

public record GetAssetScheduleByAssetIdQuery(int AssetId, DateRange Interval) : IQuery<AssetWithScheduleViewModel>;
