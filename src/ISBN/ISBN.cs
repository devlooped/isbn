using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

/// <summary>
/// Adapted to C# from https://github.com/inventaire/isbn3
/// </summary>
public partial record ISBN
{
    record Range(string Min, string Max);
    record GroupDef(string Key, string Name, Range[] Ranges);

    static readonly ConcurrentDictionary<string, ConcurrentDictionary<char, ConcurrentDictionary<string, GroupDef>>> groupsMap = new();

    static ISBN()
    {
        var raw = EmbeddedResource.GetContent("groups.js");
        var json = raw.Substring(raw.IndexOf('{'));
        var data = JObject.Parse(json);

        foreach (var prop in data.Properties())
        {
            if (prop == null || prop.Value is not JObject obj ||
                obj.Value<string>("name") is not string name ||
                obj.Property("ranges") is not JProperty ranges ||
                ranges.Value.Type != JTokenType.Array)
                continue;

            var result = new List<Range>();
            foreach (var range in ((JArray)ranges.Value).OfType<JArray>())
            {
                var values = range.Values<string>().ToArray();
                if (values.Length != 2 ||
                    values[0] == null || values[1] == null)
                    continue;

                result.Add(new Range(values[0]!, values[1]!));
            }

            var parts = prop.Name.Split('-');
            var prefix = parts[0];
            var group = parts[1];
            var groupFirstDigit = group[0];

            groupsMap.GetOrAdd(prefix, _ => new())
                .GetOrAdd(groupFirstDigit, _ => new())
                [group] = new GroupDef(group, name, result.ToArray());
        }
    }

    /// <summary>
    /// Tries parsing the given <paramref name="isbn"/> as a <see cref="ISBN"/> structure.
    /// </summary>
    /// <param name="isbn">The ISBN to parse.</param>
    /// <param name="result">The parsed result, if valid.</param>
    /// <returns><see langword="false"/> if the <paramref name="isbn"/> was not a valid ISBN. <see langword="true"/> otherwise.</returns>
    public static bool TryParse(string? isbn, [NotNullWhen(true)] out ISBN? result)
    {
        result = default;
        if (isbn == null)
            return false;

        isbn = new string(isbn.Where(c => c != ' ' && c != '-').ToArray());
        if (isbn.Length == 10)
        {
            if (!VerifyChecksum10(isbn))
                return false;

            var sb = new StringBuilder(13)
                .Append("978")
                .Append(isbn[..^1]);

            // Append irrelevant last char since it's skipped when getting the checksum
            isbn = sb.Append(GetChecksum(sb.ToString() + "0")).ToString();
        }
        else if (isbn.Length != 13 || !VerifyChecksum(isbn))
        {
            return false;
        }

        var groupData = GetGroup(isbn);
        if (groupData == null)
            return false;

        var (group, restAfterGroup) = groupData.Value;
        foreach (var range in group.Ranges)
        {
            var publisher = restAfterGroup.Substring(0, range.Min.Length);
            //// Warning: comparing strings: seems to be ok as ranges boundaries are of the same length
            //// and we are testing a publisher code of that same length
            //// (so there won't be cases of the kind '2' > '199' == true)
            if (range.Min.CompareTo(publisher) <= 0 && range.Max.CompareTo(publisher) >= 0)
            {
                var restAfterPublisher = restAfterGroup.Substring(publisher.Length);
                result = new ISBN(isbn, group.Key, group.Name, publisher, restAfterPublisher.Substring(0, restAfterPublisher.Length - 1));
                return true;
            }
        }

        return false;
    }

    readonly string isbn;

    ISBN(string isbn, string group, string groupName, string publisher, string article)
        => (this.isbn, Group, GroupName, Publisher, Article)
        = (isbn, group, groupName, publisher, article);

    /// <summary>
    /// Identifies the particular country, geographical region, or language area 
    /// participating in the ISBN system
    /// </summary>
    public string Group { get; init; }

    /// <summary>
    /// Display name of the <see href="Group"/>, such as "English language".
    /// </summary>
    public string GroupName { get; init; }

    /// <summary>
    /// Identifies the particular publisher or imprint.
    /// </summary>
    public string Publisher { get; init; }

    /// <summary>
    /// Identifies the particular edition and format of a specific title.
    /// </summary>
    public string Article { get; init; }

    /// <summary>
    /// Returns the 13-char normalized ISBN.
    /// </summary>
    public override string ToString() => isbn;

    static int GetChecksum(string isbn)
    {
        var checksum = isbn
            .Take(12)
            .Select((c, i) => (isbn[i] - '0') * (i % 2 == 0 ? 1 : 3))
            .Sum();

        var digit = (10 - checksum) % 10;
        if (digit < 0)
            digit += 10;

        return digit;
    }

    static bool VerifyChecksum(string isbn)
        => GetChecksum(isbn) == (isbn[^1] == 'X' ? 10 : isbn[^1] - '0');

    static bool VerifyChecksum10(string isbn)
    {
        var checksum = isbn
            .Take(9)
            .Select((c, i) => (isbn[i] - '0') * (10 - i))
            .Sum();

        var digit = (11 - checksum) % 11;
        if (digit < 0)
            digit += 11;

        return digit == (isbn[^1] == 'X' ? 10 : isbn[^1] - '0');
    }

    static (GroupDef group, string restAfterGroup)? GetGroup(string isbn)
    {
        var prefix = isbn.Substring(0, 3);
        if (!groupsMap.TryGetValue(prefix, out var groupMap))
            return null;

        var restAfterPrefix = isbn.Substring(3);
        var groupFirstDigit = restAfterPrefix[0];
        if (!groupMap.TryGetValue(groupFirstDigit, out var prefixMap))
            return null;

        foreach (var groupPrefix in prefixMap)
        {
            if (restAfterPrefix.StartsWith(groupPrefix.Key))
                return (groupPrefix.Value, restAfterPrefix.Substring(groupPrefix.Key.Length));
        }

        return null;
    }
}
