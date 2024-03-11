namespace Asset.Booking.SharedKernel;

public static class GenericErrors
{
    private const string EntityNotFoundMessageFormat = "The passed {0} is not valid and we cannot find it in the system.";
    public const string EntityNotFoundCode = "Entity.NotFound";

    public static readonly Error GenericEntityNotFound =
        new(EntityNotFoundCode, string.Format(EntityNotFoundMessageFormat, "entity"));

    public static Error EntityNotFound(string entityName) =>
        new(EntityNotFoundCode, string.Format(EntityNotFoundMessageFormat, entityName));

    public static Error EntityNotFound(string entityName, string identifierName, string identifier) =>
        new(EntityNotFoundCode, string.Concat(string.Format(EntityNotFoundMessageFormat, entityName), $" Using: {identifierName} - {identifier}."));

    public static Error AggregatedError(IEnumerable<Error> errors) =>
        new("Aggregate.Errors", string.Join(";", errors.Select(e => $"{e.Code}: {e.Message}")));
}
