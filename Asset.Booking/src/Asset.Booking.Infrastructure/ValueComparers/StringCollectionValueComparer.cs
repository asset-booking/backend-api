namespace Asset.Booking.Infrastructure.ValueComparers;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;

public class StringCollectionValueComparer() : ValueComparer<IEnumerable<string>>(
    (c1, c2) => CompareCollections(c1, c2),
    c => GetCollectionHashCode(c),
    c => CloneCollection(c))
{
    private static bool CompareCollections(IEnumerable<string>? c1, IEnumerable<string>? c2)
    {
        if (c1 == null && c2 == null)
            return true;
        if (c1 == null || c2 == null)
            return false;
        return c1.SequenceEqual(c2);
    }

    private static int GetCollectionHashCode(IEnumerable<string>? c)
    {
        if (c == null)
            return 0;

        unchecked
        {
            return c.Aggregate(17, (current, item) => current * 23 + (item?.GetHashCode() ?? 0));
        }
    }

    private static IEnumerable<string> CloneCollection(IEnumerable<string>? c) =>
        c == null ? [] : c.Select(x => x);
}