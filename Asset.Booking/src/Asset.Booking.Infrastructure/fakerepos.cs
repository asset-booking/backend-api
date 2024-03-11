namespace Asset.Booking.Infrastructure;
using System;
using System.Threading.Tasks;
using Domain.AssetSchedule;
using Domain.AssetSchedule.Abstractions;
using Domain.Client;
using Domain.Client.Abstractions;
using SharedKernel;
using SharedKernel.Abstractions;

public class fakeunitofwork : IUnitOfWork
{
    public void Dispose()
    {
        // TODO release managed resources here
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(1);

    public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) => Task.FromResult(true);
}

public class assetschedulerepo(IUnitOfWork unitOfWork) : IAssetScheduleRepository
{
    public IUnitOfWork UnitOfWork { get; } = unitOfWork;

    public Task AddAsync(AssetSchedule entity, CancellationToken? cancellationToken = null)
    {
        fakedb.assetSchedules.Add(entity);
        return Task.CompletedTask;
    }

    public Task<AssetSchedule?> GetByIdAsync(int id, DateRange dateRange,
        CancellationToken? cancellationToken = null)
    {
        var schedule = fakedb.assetSchedules.FirstOrDefault(s => s.Id.Equals(id));
        if (schedule is not null)
        {
            fakedb.IncludeAssetReservations(dateRange);
        }

        return Task.FromResult(schedule);
    }

    public Task<AssetSchedule?> GetByAssetIdAsync(int assetId, DateRange dateRange,
        CancellationToken? cancellationToken = null)
    {
        var schedule = fakedb.assetSchedules.FirstOrDefault(s => s.AssetId.Equals(assetId));
        if (schedule is not null)
        {
            fakedb.IncludeAssetReservations(dateRange);
        }

        return Task.FromResult(schedule);
    }

    public Task<AssetSchedule?> GetByReservationIdAsync(Guid reservationId, CancellationToken? cancellationToken = null)
    {
        var reservation = fakedb.reservations.Find(r => r.Id.Equals(reservationId));
        AssetSchedule? schedule = null;
        if (reservation is not null)
        {
            schedule = fakedb.assetSchedules.FirstOrDefault(s => s.AssetId.Equals(reservation.AssetId));
            if (schedule is not null)
            {
                fakedb.IncludeAssetReservations(reservation.Interval);
            }
        }

        return Task.FromResult(schedule);
    }

    public Task<AssetSchedule?> GetByReservationIdAsync(Guid reservationId, DateRange dateRange,
        CancellationToken? cancellationToken = null)
    {
        fakedb.IncludeAssetReservations(dateRange);

        var reservation = fakedb.reservations.Find(r => r.Id.Equals(reservationId));
        AssetSchedule? schedule = null;
        if (reservation is not null)
        {
            schedule = fakedb.assetSchedules.FirstOrDefault(s => s.AssetId.Equals(reservation.AssetId));
            if (schedule is not null)
            {
                fakedb.IncludeAssetReservations(reservation.Interval);
            }
        }

        return Task.FromResult(schedule);
    }

    public Task<Reservation?> GetReservationByIdAsync(Guid reservationId, CancellationToken? cancellationToken = null) =>
        Task.FromResult(fakedb.reservations.Find(r => r.Id.Equals(reservationId)));
}

public class fakeclientrepo(IUnitOfWork unitOfWork) : IClientRepository
{
    public IUnitOfWork UnitOfWork { get; } = unitOfWork;

    public Task AddAsync(Client entity, CancellationToken? cancellationToken = null)
    {
        fakedb.clients.Add(entity);
        return Task.CompletedTask;
    }
}
