namespace Asset.Booking.Infrastructure.Repositories;

using Domain.Client;
using Domain.Client.Abstractions;
using SharedKernel.Abstractions;

public class ClientRepository (BookingContext context) : IClientRepository
{
    private readonly BookingContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public Task AddAsync(Client client, CancellationToken? cancellationToken = null) =>
        _context.Clients
            .AddAsync(client, cancellationToken ?? default)
            .AsTask();
}