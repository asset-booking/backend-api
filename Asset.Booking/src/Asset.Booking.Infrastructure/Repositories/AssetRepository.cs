namespace Asset.Booking.Infrastructure.Repositories;

using Management.Domain.Asset;
using Management.Domain.Asset.Abstractions;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

public class AssetRepository (ManagementContext context): IAssetRepository
{
    private readonly ManagementContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public Task AddAsync(Asset asset, CancellationToken? cancellationToken = null) =>
        _context.Assets
            .AddAsync(asset, cancellationToken ?? default)
            .AsTask();

    public Task GetByIdAsync(int assetId, CancellationToken? cancellationToken = null) =>
        _context.Assets
            .SingleOrDefaultAsync(a => a.Id.Equals(assetId), cancellationToken ?? default);
}