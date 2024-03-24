namespace Asset.Booking.Infrastructure.Repositories;

using Domain.AssetSchedule;
using Domain.AssetSchedule.Abstractions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Abstractions;

public class AssetScheduleRepository(BookingContext context) : IAssetScheduleRepository
{
    private readonly BookingContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public Task<AssetSchedule?> GetByIdAsync(int id, DateRange dateRange, CancellationToken? cancellationToken = null) =>
        _context.AssetSchedules
            .Include(s => s.Reservations
                .Where(r => r.Interval.StartDate >= dateRange.StartDate && r.Interval.EndDate <= dateRange.EndDate))
            .SingleOrDefaultAsync(
                s => s.Id.Equals(id),
                cancellationToken ?? default);

    public Task<AssetSchedule?> GetByAssetIdAsync(int assetId, DateRange dateRange, CancellationToken? cancellationToken = null) =>
        _context.AssetSchedules
            .Include(s => s.Reservations
                .Where(r => r.Interval.StartDate >= dateRange.StartDate && r.Interval.EndDate <= dateRange.EndDate))
            .SingleOrDefaultAsync(
                s => s.AssetId.Equals(assetId),
                cancellationToken ?? default);

    public Task<AssetSchedule?> GetByReservationIdAsync(Guid reservationId, CancellationToken? cancellationToken = null) => 
        _context.AssetSchedules
            .Include(s => s.Reservations
                .Where(r => r.Id.Equals(reservationId)))
            .SingleOrDefaultAsync(
                s => s.Reservations.Any(r => r.Id.Equals(reservationId)),
                cancellationToken ?? default);

    public Task<AssetSchedule?> GetByReservationIdAsync(Guid reservationId, DateRange dateRange, CancellationToken? cancellationToken = null) =>
        _context.AssetSchedules
            .Include(s => s.Reservations
                .Where(r => r.Interval.StartDate >= dateRange.StartDate && r.Interval.EndDate <= dateRange.EndDate))
            .SingleOrDefaultAsync(
                s => s.Reservations.Any(r => r.Id.Equals(reservationId)),
                cancellationToken ?? default);

    public Task<Reservation?> GetReservationByIdAsync(Guid reservationId, CancellationToken? cancellationToken = null) =>
        _context.Reservations.SingleOrDefaultAsync(
            r => r.Id.Equals(reservationId),
            cancellationToken ?? default);
}