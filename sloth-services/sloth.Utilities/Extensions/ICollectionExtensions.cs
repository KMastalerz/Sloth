namespace sloth.Utilities.Extensions;
public static class ICollectionExtensions
{
    public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
    {
        foreach (T item in source)
        {
            destination.Add(item);
        }
    }
}
