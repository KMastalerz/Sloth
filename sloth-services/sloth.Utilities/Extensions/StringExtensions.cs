namespace sloth.Utilities.Extensions;
public static class StringExtensions
{
    public static string CapitalizeFirstLetter(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return char.ToUpper(value[0]) + value[1..];
    }
    public static string ToCamelCase(this string value)
    {
        // Trim leading and trailing spaces and split the input by spaces
        var words = value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // If there's only one word, return it with the first letter lowercased
        if (words.Length == 1)
        {
            return char.ToLower(words[0][0]) + words[0].Substring(1);
        }

        // Otherwise, convert the first word to lowercase and capitalize the first letter of the rest
        for (int i = 0; i < words.Length; i++)
        {
            if (i == 0)
            {
                words[i] = words[i].ToLower(); // Convert the first word to lowercase
            }
            else
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            }
        }

        // Join the words together with no spaces to form the camel case string
        return string.Join("", words);
    }
}
