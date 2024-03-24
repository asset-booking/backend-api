namespace Asset.Booking.Application.Reservations.Queries;

using Abstractions.Configurations;
using Abstractions.Messaging;
using Dapper;
using Domain.AssetSchedule;
using Domain.Client;
using Npgsql;
using SharedKernel;
using ViewModels;

public class GetReservationByIdQueryHandler(IApplicationConfiguration configuration)
    : IQueryHandler<GetReservationByIdQuery, ReservationViewModel>
{
    public async Task<Result<ReservationViewModel>> Handle(
        GetReservationByIdQuery request,
        CancellationToken cancellationToken)
    {
        await using NpgsqlConnection connection = new(configuration.SqlConnectionString);
        var reservationQuery = @"
            select
                r.*,
                sch.asset_id,
                c.company_name,
                c.email,
                coord.number coordinator_phone,
                comp.number company_phone
            from reservations r
            join asset_schedules sch on sch.id = r.schedule_id
            join clients c on r.client_id = c.id
            left join lateral (
                select number
                from phone_numbers p
                where p.client_id=r.client_id and type='Coordinator' limit 1) coord on true
            left join lateral (
                select number
                from phone_numbers p
                where p.client_id=r.client_id and type='Company' limit 1) comp on true
            where r.id=@reservationId
        ";

        var reservation =
            await connection.QuerySingleOrDefaultAsync(reservationQuery, new { reservationId = request.Id });

        if (reservation is null)
        {
            return Result<ReservationViewModel>
                .Failure(GenericErrors.EntityNotFound(
                    nameof(reservation),
                    nameof(request.Id),
                    request.Id.ToString()));
        }

        ReservationClientViewModel clientViewModel = new ReservationClientViewModel(
            reservation.client_id,
            reservation.company_name,
            reservation.company_phone,
            reservation.email,
            reservation.coordinator_phone);

        CostsViewModel costViewModel = new CostsViewModel(
            reservation.price_per_person,
            reservation.service_fee,
            reservation.nr_of_nights,
            reservation.vat_percent,
            reservation.vat_cost,
            reservation.total_cost);

        Result<ReservationViewModel> response = Result<ReservationViewModel>.Success(
            new ReservationViewModel(
                reservation.id,
                reservation.asset_id,
                reservation.schedule_id,
                reservation.interval_start,
                reservation.interval_end,
                Enumeration.FromName<Status>(reservation.status).Id,
                "current moderator",
                clientViewModel,
                costViewModel
            ));

        return response;
    }
}