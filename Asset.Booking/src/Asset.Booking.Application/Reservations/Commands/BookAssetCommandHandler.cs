namespace Asset.Booking.Application.Reservations.Commands;

using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.SharedKernel;
using Domain.AssetSchedule;
using Domain.AssetSchedule.Abstractions;
using Domain.AssetSchedule.Validation;

public class BookAssetCommandHandler(IAssetScheduleRepository assetScheduleRepository)
    : BaseCommand(assetScheduleRepository), ICommandHandler<BookAssetCommand>
{
    public async Task<Result> Handle(BookAssetCommand request, CancellationToken cancellationToken)
    {
        var dto = request.BookAssetDto;

        var assetSchedule = await assetScheduleRepository.GetByIdAsync(
            dto.ScheduleId,
            new DateRange(dto.StartDate.AddDays(-1), dto.EndDate.AddDays(1)),
            cancellationToken);

        if (assetSchedule is null)
            return ScheduleNotFoundError(nameof(dto.ScheduleId), dto.ScheduleId);

        var status = Enumeration.FromValue<Status>(dto.StatusId);
        if (status is null) return BookingErrors.Reservations.InvalidStatus;

        return await ExecuteCommandAndSaveAsync(
            () =>
            {
                var interval = new DateRange(dto.StartDate, dto.EndDate);

                var cost = new Cost(
                    dto.PricePerPerson,
                    dto.ServiceFee,
                    dto.NumberOfPeople,
                    interval.IntervalNights,
                    dto.VatPercentage);

                assetSchedule.BookReservation(
                    request.ReservationId,
                    dto.ClientId,
                    assetSchedule.AssetId,
                    status,
                    interval,
                    cost);
            },
            cancellationToken);
    }
}
