namespace Asset.Management.Domain.Asset.Abstractions;

using Booking.SharedKernel.Abstractions;

public interface ICategoryRepository : IRepository<Category>
{
    Task AddAsync(Category category, CancellationToken? cancellationToken = null);
    Task GetById(int categoryId, CancellationToken? cancellationToken = null);
}