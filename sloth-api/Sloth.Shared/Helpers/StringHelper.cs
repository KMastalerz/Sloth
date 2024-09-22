namespace Sloth.Shared.Helpers;
public static class StringHelper
{
    public static string CapitalizeFirstLetter(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return char.ToUpper(value[0]) + value[1..];
    }
}
