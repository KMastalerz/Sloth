using System.Reflection;
namespace Sloth.Shared.Helpers;
public static class GenericHelper
{
    public static bool HasChanges<T>(this T newObject, T originalObject, List<string>? excludedProperties = null)
    {
        if (newObject == null || originalObject == null)
            throw new ArgumentNullException("Objects to compare cannot be null.");

        excludedProperties ??= new List<string>();

        // Get all properties of the object type
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            // Skip excluded properties
            if (excludedProperties.Contains(property.Name))
                continue;

            // Get the values of the property for both objects
            var newValue = property.GetValue(newObject);
            var originalValue = property.GetValue(originalObject);

            // Check if values are equal, accounting for nulls
            if (!AreValuesEqual(newValue, originalValue))
                return true; // A difference is found, so return true
        }

        return false; // No differences found, return false
    }

    private static bool AreValuesEqual(object? newValue, object? originalValue)
    {
        if (newValue == null && originalValue == null)
            return true; // Both are null, considered equal

        if (newValue == null || originalValue == null)
            return false; // One is null, the other is not, considered not equal

        return newValue.Equals(originalValue); // Use Equals to compare values
    }
}
