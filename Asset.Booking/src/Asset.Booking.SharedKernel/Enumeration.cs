namespace Asset.Booking.SharedKernel;
using System;
using System.Reflection;

public abstract class Enumeration(string name, int id) : IComparable
{
    public string Name { get; } = name;
    public int Id { get; } = id;

    public int CompareTo(object? obj) =>
        obj is Enumeration en
            ? Id.CompareTo(en.Id)
            : throw new ArgumentException($"Not an instance of {nameof(Enumeration)}", nameof(obj));

    public override string ToString() => Name;

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration en)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return Id.Equals(en.Id);
    }

    public static bool operator ==(Enumeration? left, Enumeration? right) =>
        left?.Equals(right) ?? false;

    public static bool operator !=(Enumeration? left, Enumeration? right) =>
        !(left == right);

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(default))
            .Cast<T>();

    public static T? FromValue<T>(int value) where T : Enumeration =>
        FindWithPredicate<T, int>(value, en => en.Id);

    public static T? FromName<T>(string name) where T : Enumeration =>
        FindWithPredicate<T, string>(name, en => en.Name);

    private static T? FindWithPredicate<T, TVal>(
        TVal value,
        Func<T, TVal> getValue) where T : Enumeration => 
        GetAll<T>().FirstOrDefault(e => getValue(e)!.Equals(value));
}
