namespace Asset.Booking.Infrastructure;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using Domain.AssetSchedule;
using Domain.Client;
using SharedKernel;

public static class fakedb
{
    public static List<dynamic> moderators = new List<dynamic>()
    {
        getModerator(1, "Bobby"),
        getModerator(2, "John"),
    };

    private static ExpandoObject getModerator(int id, string name)
    {
        dynamic obj = new ExpandoObject();
        obj.Id = id;
        obj.Name = name;

        return obj;
    }

    public static List<dynamic> assets = new List<dynamic>()
    {
        getAsset(1, "CAT0 SB10 SB20", "B 1","king_bed", string.Empty),
        getAsset(2, "CAT0 SB10 SB20", "B 2","king_bed", string.Empty),
        getAsset(3, "CAT2 SB12 SB22", "B 1","single_bed", string.Empty),
        getAsset(4, "CAT2 SB12 SB22", "B 2","single_bed", string.Empty),
        getAsset(5, "CAT2 SB12 SB22", "B 3","king_bed", "No Smoking"),
        getAsset(6, "CAT5 SB15 SB25", "B 1","king_bed", string.Empty),
        getAsset(7, "CAT6 SB16 SB26", "B 1","single_bed", "No Smoking"),
        getAsset(8, "CAT7 SB17 SB27", "B 1","single_bed", "No Smoking"),
        getAsset(9, "CAT8 SB18 SB28", "B 1","king_bed", string.Empty),
        getAsset(10,"CAT9 SB19 SB29", "B 1","single_bed", string.Empty),
    };

    private static ExpandoObject getAsset(int id, string categ, string spec, string icon, string note)
    {
        dynamic obj = new ExpandoObject();
        obj.Id = id;

        dynamic det = new ExpandoObject();
        det.CategoryReference = categ;
        det.Specification = spec;
        det.SpecificationIcons = icon;
        det.Notes = note;
        det.NotesIcons = null;

        obj.Details = det;

        return obj;
    }

    public static List<Client> clients = new()
    {
        new Client(
            Guid.NewGuid(),
            "Company Name 1",
            new Contacts("client1@mail.com", new []
            {
                new PhoneNumber("+373 79 178 786", PhoneNumberType.Company),
                new PhoneNumber("+373 80 123 433", PhoneNumberType.Coordinator)
            }),
            new Address("Chisinau", "MD-3001", "Vissarion Belinksi", "13")),
        new Client(
            Guid.NewGuid(),
            "Company Name 22",
            new Contacts("client22@mail.com", new []
            {
                new PhoneNumber("+49 788 098 123", PhoneNumberType.Coordinator)
            }),
            new Address("Brasov", "500164")),
        new Client(
            Guid.NewGuid(),
            "Company Name 333",
            new Contacts("client333@mail.com", new []
            {
                new PhoneNumber("+49 788 098 123", PhoneNumberType.Coordinator)
            })),
        new Client(
            Guid.NewGuid(),
            "Company Name 4444",
            new Contacts("client4444@mail.com", new []
            {
                new PhoneNumber("+49 788 098 123", PhoneNumberType.Coordinator)
            }))
    };

    public static List<AssetSchedule> assetSchedules = assets.Select(a => new AssetSchedule(a.Id)).ToList();

    public static List<Reservation> reservations = new()
    {
        new Reservation(
            Guid.NewGuid(),
            moderators[0].Id,
            clients[0].Id,
            assets[0].Id,
            Status.Paid,
            new DateRange(new DateTime(2024, 1, 1), 7),
            new Cost(12.2M, 20M, 1, 6, 7)),
        new Reservation(
            Guid.NewGuid(),
            moderators[0].Id,
            clients[2].Id,
            assets[0].Id,
            Status.Enquiry,
            new DateRange(new DateTime(2024, 1, 8), 5),
            new Cost(14.2M, 20M, 1, 4, 7)),
        new Reservation(
            Guid.NewGuid(),
            moderators[1].Id,
            clients[3].Id,
            assets[1].Id,
            Status.DownPayment,
            new DateRange(new DateTime(2024, 1, 3), 10),
            new Cost(10M, 20M, 1, 9, 7)),
        new Reservation(
            Guid.NewGuid(),
            moderators[0].Id,
            clients[3].Id,
            assets[2].Id,
            Status.Paid,
            new DateRange(new DateTime(2024, 1, 6), 10),
            new Cost(11.11M, 20M, 1, 9, 7)),
        new Reservation(
            Guid.NewGuid(),
            moderators[1].Id,
            clients[1].Id,
            assets[3].Id,
            Status.Paid,
            new DateRange(new DateTime(2024, 1, 3), 5),
            new Cost(10M, 20M, 1, 4, 7)),
        new Reservation(
            Guid.NewGuid(),
            moderators[1].Id,
            clients[3].Id,
            assets[3].Id,
            Status.Open,
            new DateRange(new DateTime(2024, 1, 8), 4),
            new Cost(10M, 20M, 1, 3, 7)),
        new Reservation(
            Guid.NewGuid(),
            moderators[0].Id,
            clients[2].Id,
            assets[4].Id,
            Status.Enquiry,
            new DateRange(new DateTime(2024, 1, 2), 10),
            new Cost(9M, 20M, 1, 9, 7))
    };

public static void IncludeAssetReservations(DateRange interval)
    {
        foreach (AssetSchedule assetSchedule in assetSchedules)
        {
            var scheduledReservations = reservations.Where(r =>
                r.AssetId.Equals(assetSchedule.AssetId) &&
                interval.Overlaps(r.Interval));

            foreach (Reservation sheetReservation in scheduledReservations)
            {
                try
                {
                    assetSchedule.BookReservation(
                        sheetReservation.Id,
                        sheetReservation.ClientId,
                        sheetReservation.ModeratorId,
                        sheetReservation.Status,
                        sheetReservation.Interval,
                        sheetReservation.Cost);
                }
                catch
                {

                }
            }
        }
    }
}
