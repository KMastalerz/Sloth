namespace Sloth.Domain.Entities;
public class Translation
{
    public string ENUText { get; set; } = default!;
    /// <summary>
    /// references Language
    /// </summary>
    public string LanguageCode { get; set; } = default!;
    public string TranslatedText { get; set; } = default!;
}
