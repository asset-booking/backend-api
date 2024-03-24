namespace Asset.Booking.Application.Reservations.Commands;

using Abstractions.Messaging;
using Domain.AssetSchedule.Abstractions;
using SharedKernel;

public class CancelReservationCommandHandler(IAssetScheduleRepository assetScheduleRepository)
    : BaseCommand(assetScheduleRepository), ICommandHandler<CancelReservationCommand>
{
    public async Task<Result> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await AssetScheduleRepository
            .GetReservationByIdAsync(request.ReservationId, cancellationToken);

        if (reservation is null)
            return ReservationNotFoundError(nameof(request.ReservationId), request.ReservationId);

        return await ExecuteCommandAndSaveAsync(
            () => reservation.Cancel(),
            cancellationToken);
    }
}