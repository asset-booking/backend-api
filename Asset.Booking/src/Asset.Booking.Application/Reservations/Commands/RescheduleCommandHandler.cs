namespace Asset.Booking.Application.Reservations.Commands;

using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.SharedKernel;
using Domain.AssetSchedule.Abstractions;

public class RescheduleCommandHandler(IAssetScheduleRepository assetScheduleRepository)
    : BaseCommand(assetScheduleRepository), ICommandHandler<RescheduleCommand>
{
    public async Task<Result> Handle(RescheduleCommand request, CancellationToken cancellationToken)
    {
        var assetSchedule = await AssetScheduleRepository.GetByReservationIdAsync(
            request.ReservationId,
            GetReadInterval(request.OldInterval, request.NewInterval),
            cancellationToken);

        if (assetSchedule is null)
            return ScheduleNotFoundError(nameof(request.ReservationId), request.ReservationId);

        return await ExecuteCommandAndSaveAsync(
            () => assetSchedule.RescheduleReservation(request.ReservationId, request.NewInterval),
            cancellationToken);
    }

    private DateRange GetReadInterval(DateRange currenInterval, DateRange newInterval) =>
        new DateRange(
            new DateTime(Math.Min(currenInterval.StartDate.Ticks, newInterval.StartDate.Ticks)),
            new DateTime(Math.Max(currenInterval.EndDate.Ticks, newInterval.EndDate.Ticks)));
}
