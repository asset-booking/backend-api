namespace Asset.Booking.Application.Reservations.Commands;

using Asset.Booking.Domain.AssetSchedule.Abstractions;
using SharedKernel.Exceptions;
using Domain.AssetSchedule;
using SharedKernel;

public abstract class BaseCommand(IAssetScheduleRepository assetScheduleRepository)
{
    protected readonly IAssetScheduleRepository AssetScheduleRepository = assetScheduleRepository;
    protected async Task<Result> ExecuteCommandAndSaveAsync(
        Action action,
        CancellationToken cancellationToken)
    {
        try
        {
            action.Invoke();
            await AssetScheduleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (AssetBookingException ex) when (ex.Error is not null)
        {
            return ex.Error;
        }
        catch (Exception ex)
        {
            return new Error("Reservations.Error", ex.Message);
        }

        return Result.Success();
    }

    protected static Error ScheduleNotFoundError<T>(string identifierName, T identifier) =>
        EntityNotFoundError(nameof(AssetSchedule), identifierName, identifier);

    protected static Error ReservationNotFoundError<T>(string identifierName, T identifier) =>
        EntityNotFoundError(nameof(Reservation), identifierName, identifier);

    private static Error EntityNotFoundError<T>(string entityName, string identifierName, T identifier)
    {
        var identifierValue = identifier?.ToString();

        return identifierValue is null
            ? GenericErrors.EntityNotFound(entityName)
            : GenericErrors.EntityNotFound(
                entityName,
                identifierName,
                identifierValue);
    }
}
