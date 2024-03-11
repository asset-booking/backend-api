namespace Asset.Booking.SharedKernel;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override int GetHashCode() =>
        GetEqualityComponents()
            .Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);

    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject valueObject)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject? left, ValueObject? right) =>
        left?.Equals(right) ?? false;

    public static bool operator !=(ValueObject? left, ValueObject? right) =>
        !(left == right);
}
