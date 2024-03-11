namespace Asset.Booking.SharedKernel;

using System.Diagnostics.CodeAnalysis;

public abstract class Entity<T> : IEquatable<Entity<T>> where T : struct
{
    private int? _hashCode;

    public T Id { get; protected set; }

    public bool IsTransient => Id.Equals(default(T));

    [SuppressMessage("ReSharper", "BaseObjectGetHashCodeCallInGetHashCode")]
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        if (IsTransient) return base.GetHashCode();

        _hashCode ??= Id.GetHashCode() ^ 31;

        return _hashCode.Value;
    }

    public bool Equals(Entity<T>? other)
    {
        if (other is null)
            return false;

        if (IsTransient || other.IsTransient)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj) =>
        obj is Entity<T> entity && Equals(entity);

    public static bool operator ==(Entity<T>? left, Entity<T>? right) =>
        left?.Equals(right) ?? false;

    public static bool operator !=(Entity<T>? left, Entity<T>? right) =>
        !(left == right);
}