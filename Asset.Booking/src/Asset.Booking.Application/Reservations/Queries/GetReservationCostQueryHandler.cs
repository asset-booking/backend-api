namespace Asset.Booking.Application.Reservations.Queries;

using Abstractions.Messaging;
using Domain.AssetSchedule;
using SharedKernel;
using SharedKernel.Exceptions;
using ViewModels;

public class GetReservationCostQueryHandler : IQueryHandler<GetReservationCostQuery, CostsViewModel>
{
    public Task<Result<CostsViewModel>> Handle(GetReservationCostQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cost = new Cost(
                request.Cost.PricePerPerson,
                request.Cost.ServiceFee,
                request.Cost.NumberOfPeople,
                request.Cost.NumberOfNights,
                request.Cost.VatPercentage);

            var costViewModel = new CostsViewModel(
                cost.PricePerPerson,
                cost.ServiceFee,
                cost.NumberOfNights,
                cost.VatPercentage,
                cost.VatCost,
                cost.TotalCost);

            return Task.FromResult(Result<CostsViewModel>.Success(costViewModel));
        }
        catch (AssetBookingException ex) when (ex.Error is not null)
        {
            return Task.FromResult(Result<CostsViewModel>.Failure(ex.Error));
        }
        catch
        {
            return Task.FromResult(Result<CostsViewModel>
                .Failure(new Error("Reservation.Cost", "Could not calculate the cost.")));
        }
    }
}