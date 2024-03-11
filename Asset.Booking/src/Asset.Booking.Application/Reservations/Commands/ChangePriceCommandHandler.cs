namespace Asset.Booking.Application.Reservations.Commands;
using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.Domain.AssetSchedule.Abstractions;
using SharedKernel;

public class ChangePriceCommandHandler(IAssetScheduleRepository assetScheduleRepository)
    : BaseCommand(assetScheduleRepository), ICommandHandler<ChangePriceCommand>
{
    public async Task<Result> Handle(ChangePriceCommand request, CancellationToken cancellationToken)
    {
        var reservation = await assetScheduleRepository
            .GetReservationByIdAsync(request.ReservationId, cancellationToken);

        if (reservation is null)
            return ReservationNotFoundError(nameof(request.ReservationId), request.ReservationId);

        return await ExecuteCommandAndSaveAsync(
            () => reservation.ChangePricePerPerson(request.NewPricePerPerson),
            cancellationToken);
    }
}
