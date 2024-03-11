namespace Asset.Booking.Domain.AssetSchedule;
using SharedKernel;
using SharedKernel.Abstractions;

public class Moderator(string name) : Entity<int>, IAggregateRoot
{
    public string Name { get; } = name;
}
