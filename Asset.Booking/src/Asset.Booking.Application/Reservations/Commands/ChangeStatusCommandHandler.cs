namespace Asset.Booking.Application.Reservations.Commands;

using Abstractions.Messaging;
using Domain.AssetSchedule.Abstractions;
using SharedKernel;

public class ChangeStatusCommandHandler(IAssetScheduleRepository assetScheduleRepository)
    : BaseCommand(assetScheduleRepository), ICommandHandler<ChangeStatusCommand>
{
    public async Task<Result> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var reservation = await AssetScheduleRepository
            .GetReservationByIdAsync(request.ReservationId, cancellationToken);

        if (reservation is null)
            return ReservationNotFoundError(nameof(request.ReservationId), request.ReservationId);

        return await ExecuteCommandAndSaveAsync(
            () => reservation.ChangeStatus(request.NewStatusId),
            cancellationToken);
    }
}