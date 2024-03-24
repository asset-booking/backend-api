namespace Asset.Management.Domain.Asset.Abstractions;

using Booking.SharedKernel.Abstractions;

public interface IAssetRepository: IRepository<Asset>
{
    Task AddAsync(Asset asset, CancellationToken? cancellationToken = null);
    Task GetByIdAsync(int assetId, CancellationToken? cancellationToken = null);
}