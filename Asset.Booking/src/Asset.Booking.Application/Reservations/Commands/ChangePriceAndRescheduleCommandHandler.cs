namespace Asset.Booking.Application.Reservations.Commands;
using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.Domain.AssetSchedule.Abstractions;
using SharedKernel;

public class ChangePriceAndRescheduleCommandHandler(IAssetScheduleRepository assetScheduleRepository)
    : BaseCommand(assetScheduleRepository), ICommandHandler<ChangePriceAndRescheduleCommand>
{
    public async Task<Result> Handle(ChangePriceAndRescheduleCommand request, CancellationToken cancellationToken)
    {
        var assetSchedule = await assetScheduleRepository.GetByReservationIdAsync(
            request.ReservationId,
            request.NewInterval);

        if (assetSchedule is null)
            return ScheduleNotFoundError(nameof(request.ReservationId), request.ReservationId);

        var reservation = assetSchedule.GetReservation(request.ReservationId);
        if (reservation is null)
            return ReservationNotFoundError(nameof(request.ReservationId), request.ReservationId);

        return await ExecuteCommandAndSaveAsync(
            () =>
            {
                assetSchedule.RescheduleReservation(request.ReservationId, request.NewInterval);
                reservation.ChangePricePerPerson(request.NewPricePerPerson);
            },
            cancellationToken);
    }
}
