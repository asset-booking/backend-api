namespace Asset.Booking.Domain.Client.Abstractions;
using SharedKernel.Abstractions;

public interface IClientRepository: IRepository<Client>
{
    Task AddAsync(Client client, CancellationToken? cancellationToken = null);
}
