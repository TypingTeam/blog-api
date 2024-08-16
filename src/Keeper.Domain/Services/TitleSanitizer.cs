using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Keeper.Domain.Services;

public partial class TitleSanitizer
{
    [GeneratedRegex(
        @"[^A-Za-z0-9\s]",
        RegexOptions.CultureInvariant,
        matchTimeoutMilliseconds: 1000)]
    private static partial Regex MatchIfSpecialCharactersExist();

    [GeneratedRegex(
        @"\s+",
        RegexOptions.CultureInvariant,
        matchTimeoutMilliseconds: 1000)]
    private static partial Regex MatchIfAdditionalSpacesExist();

    [GeneratedRegex(
        @"\s",
        RegexOptions.CultureInvariant,
        matchTimeoutMilliseconds: 1000)]
    private static partial Regex MatchIfSpaceExist();
    
    public static string GenerateSlug(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return title;
        }

        var normalizedTitle = title.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedTitle.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
        {
            stringBuilder.Append(c);
        }

        var cleanTitle = stringBuilder
            .ToString()
            .Normalize(NormalizationForm.FormC)
            .ToLower(CultureInfo.CurrentCulture);

        cleanTitle = MatchIfSpecialCharactersExist().Replace(cleanTitle, "");
        cleanTitle = MatchIfAdditionalSpacesExist().Replace(cleanTitle, " ");
        cleanTitle = MatchIfSpaceExist().Replace(cleanTitle, "-");

        return cleanTitle.Trim();
    }
}
