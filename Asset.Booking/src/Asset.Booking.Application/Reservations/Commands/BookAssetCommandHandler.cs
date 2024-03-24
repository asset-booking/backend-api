namespace Asset.Booking.Application.Reservations.Commands;

using System.Threading.Tasks;
using Abstractions.Messaging;
using SharedKernel;
using Domain.AssetSchedule;
using Domain.AssetSchedule.Abstractions;
using Domain.AssetSchedule.Validation;

public class BookAssetCommandHandler(IAssetScheduleRepository assetScheduleRepository)
    : BaseCommand(assetScheduleRepository), ICommandHandler<BookAssetCommand>
{
    public async Task<Result> Handle(BookAssetCommand request, CancellationToken cancellationToken)
    {
        var dto = request.BookAssetDto;
        
        var status = Enumeration.FromValue<Status>(dto.StatusId);
        if (status is null) return BookingErrors.Reservations.InvalidStatus;

        var errors = new List<Error>();
        foreach (var ids in dto.ScheduleReservationIds)
        {
            var assetSchedule = await AssetScheduleRepository.GetByIdAsync(
                ids.ScheduleId,
                new DateRange(dto.StartDate.AddDays(-1), dto.EndDate.AddDays(1)),
                cancellationToken);

            if (assetSchedule is null)
            {
                errors.Add(ScheduleNotFoundError(nameof(ids.ScheduleId), ids.ScheduleId));
                continue;
            }
            
            var bookingResult = await ExecuteCommandAndSaveAsync(
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
                        ids.ReservationId,
                        dto.ClientId,
                        assetSchedule.AssetId,
                        status,
                        interval,
                        cost);
                },
                cancellationToken);

            if (!bookingResult.IsSuccess)
            {
                errors.Add(bookingResult.Error);
            }
        }

        return errors.Any() 
            ? GenericErrors.AggregatedError(errors)
            : Result.Success();
    }
}
