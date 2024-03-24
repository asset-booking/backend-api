namespace Asset.Booking.Infrastructure.Repositories;

using Management.Domain.Asset;
using Management.Domain.Asset.Abstractions;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

public class CategoryRepository(ManagementContext context) : ICategoryRepository
{
    private readonly ManagementContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;
    
    public Task AddAsync(Category category, CancellationToken? cancellationToken = null) =>
        _context.Categories
            .AddAsync(category, cancellationToken ?? default)
            .AsTask();


    public Task GetById(int categoryId, CancellationToken? cancellationToken = null) =>
        _context.Categories
            .Include(c => c.SubCategories)
            .SingleOrDefaultAsync(c => c.Id.Equals(categoryId));
}