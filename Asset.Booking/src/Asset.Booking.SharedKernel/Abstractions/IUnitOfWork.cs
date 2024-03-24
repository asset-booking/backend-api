namespace Asset.Booking.SharedKernel.Abstractions;
using System.Threading.Tasks;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
