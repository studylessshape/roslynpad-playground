var notDuplicate = new int[] { 1, 2, 3, 4, 5, 6 };
var duplicate = new int[] { 1, 2, 3, 2, 5, 6 };

notDuplicate.HasDuplicate().Dump();
notDuplicate.FirstOrDefaultDuplicate().Dump();

duplicate.HasDuplicate().Dump();
duplicate.FirstOrDefaultDuplicate().Dump();

public static class IEnumerableExtensions
{
    public static bool HasDuplicate<T>(this IEnumerable<T> source)
    {
        HashSet<T> seen = [];
        foreach (T item in source)
        {
            if (!seen.Add(item))
                return true;
        }
        return false;
    }

    public static T? FirstOrDefaultDuplicate<T>(this IEnumerable<T> source)
    {
        HashSet<T> seen = [];
        foreach (T item in source)
        {
            if (!seen.Add(item))
                return item;
        }
        return default;
    }
}