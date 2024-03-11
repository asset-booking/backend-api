namespace Asset.Booking.Application.Reservations.Queries;
using System.Threading.Tasks;
using Abstractions.Messaging;
using Domain.Client;
using Infrastructure;
using SharedKernel;
using ViewModels;

public class GetReservationByIdQueryHandler : IQueryHandler<GetReservationByIdQuery, ReservationViewModel>
{
    public Task<Result<ReservationViewModel>> Handle(
        GetReservationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var reservation = Infrastructure.fakedb.reservations.Find(r => r.Id.Equals(request.Id));
        if (reservation is null)
            return Task.FromResult(
                Result<ReservationViewModel>
                    .Failure(GenericErrors.EntityNotFound(nameof(reservation), nameof(request.Id), request.Id.ToString())));

        var client = fakedb.clients.Find(c => c.Id.Equals(reservation.ClientId));
        if (client is null)
            return Task.FromResult(
                Result<ReservationViewModel>
                    .Failure(GenericErrors.EntityNotFound(nameof(client), nameof(client.Id), reservation.ClientId.ToString())));

        var clientViewModel = new ReservationClientViewModel(
            client.Id,
            client.CompanyName,
            GetPhoneNumber(client, PhoneNumberType.Company),
            client.Contacts.Email,
            GetPhoneNumber(client, PhoneNumberType.Coordinator));

        var costViewModel = new CostsViewModel(
            reservation.Cost.PricePerPerson,
            reservation.Cost.ServiceFee,
            reservation.Cost.VatPercentage,
            reservation.Cost.VatCost,
            reservation.Cost.TotalCost);

        var response = Result<ReservationViewModel>.Success(
            new ReservationViewModel(
                reservation.Id,
                reservation.AssetId, 
                reservation.Interval.StartDate, 
                reservation.Interval.EndDate,
                reservation.Status.Id,
                clientViewModel,
                costViewModel
                ));

        return Task.FromResult(response);
    }

    private string GetPhoneNumber(Client client, PhoneNumberType type) =>
        client.Contacts.PhoneNumbers.FirstOrDefault(p => p.Type.Equals(type))?.ToString() ??
        string.Empty;
}
