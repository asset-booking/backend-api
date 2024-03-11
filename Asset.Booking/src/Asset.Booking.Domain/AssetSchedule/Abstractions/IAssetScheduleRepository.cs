namespace Asset.Booking.Domain.AssetSchedule.Abstractions;

using SharedKernel;
using SharedKernel.Abstractions;

public interface IAssetScheduleRepository: IRepository<AssetSchedule>
{
    Task AddAsync(AssetSchedule entity, CancellationToken? cancellationToken = null);
    Task<AssetSchedule?> GetByIdAsync(int id, DateRange dateRange, CancellationToken? cancellationToken = null);
    Task<AssetSchedule?> GetByAssetIdAsync(int assetId, DateRange dateRange, CancellationToken? cancellationToken = null);
    Task<AssetSchedule?> GetByReservationIdAsync(Guid reservationId, CancellationToken? cancellationToken = null);
    Task<AssetSchedule?> GetByReservationIdAsync(Guid reservationId, DateRange dateRange, CancellationToken? cancellationToken = null);
    Task<Reservation?> GetReservationByIdAsync(Guid reservationId, CancellationToken? cancellationToken = null);
}