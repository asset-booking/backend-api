namespace Asset.Booking.Application.Reservations.Commands;

using Asset.Booking.Domain.AssetSchedule.Abstractions;
using Asset.Booking.SharedKernel.Exceptions;
using Domain.AssetSchedule;
using SharedKernel;

public abstract class BaseCommand(IAssetScheduleRepository assetScheduleRepository)
{
    public async Task<Result> ExecuteCommandAndSaveAsync(
        Action action,
        CancellationToken cancellationToken)
    {
        try
        {
            action.Invoke();
        }
        catch (AssetBookingException ex) when (ex.Error is not null)
        {
            return ex.Error;
        }
        catch (Exception ex)
        {
            return new Error("Reservations.Error", ex.Message);
        }

        await assetScheduleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public static Error ScheduleNotFoundError<T>(string identifierName, T identifier) =>
        EntityNotFoundError(nameof(AssetSchedule), identifierName, identifier);

    public static Error ReservationNotFoundError<T>(string identifierName, T identifier) =>
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
