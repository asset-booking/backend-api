namespace Asset.Booking.Application.AssetSchedules.Queries.Shared;
using System.Linq;
using Asset.Booking.Domain.AssetSchedule;
using Asset.Booking.Domain.Client;
using Asset.Booking.Infrastructure;

public static class ReservationExtensions
{
    public static string GetModeratorName(this Reservation reservation) =>
        fakedb.moderators.FirstOrDefault(m => m.Id.Equals(reservation.ModeratorId))?.Name ?? string.Empty;

    public static string GetCoordinatorPhone(this Reservation reservation)
    {
        var client = fakedb.clients.Single(c => c.Id.Equals(reservation.ClientId));
        return client.Contacts.PhoneNumbers
            .FirstOrDefault(p => p.Type.Equals(PhoneNumberType.Coordinator))?.Number ?? string.Empty;
    }
}
