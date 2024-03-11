namespace Asset.Booking.Application.AssetSchedules.Queries;
using Abstractions.Messaging;
using SharedKernel;
using ViewModels;

public record GetAssetScheduleQuery(int ScheduleId, DateRange Interval) : IQuery<AssetWithScheduleViewModel>;
