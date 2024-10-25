using System.Collections.ObjectModel;

namespace Sloth.Shared.Helpers;
public static class ListHelpers
{
    public static bool HasElements(this List<dynamic>? list)
    {
        if (list is null) return false;
        return list.Count() > 0;
    }
    public static bool HasElements(this IEnumerable<dynamic>? list)
    {
        if (list is null) return false;
        return list.Count() > 0;
    }
    public static bool HasElements(this ObservableCollection<dynamic>? list)
    {
        if (list is null) return false;
        return list.Count() > 0;
    }
}
