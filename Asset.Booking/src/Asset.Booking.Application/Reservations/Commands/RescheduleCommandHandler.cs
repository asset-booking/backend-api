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
        var assetSchedule = await assetScheduleRepository.GetByReservationIdAsync(
            request.ReservationId,
            request.NewInterval);

        if (assetSchedule is null)
            return ScheduleNotFoundError(nameof(request.ReservationId), request.ReservationId);

        return await ExecuteCommandAndSaveAsync(
            () => assetSchedule.RescheduleReservation(request.ReservationId, request.NewInterval),
            cancellationToken);
    }
}
