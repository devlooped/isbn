using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json.Linq;
using Scriban;
using static System.Net.Mime.MediaTypeNames;

public record Range(string Min, string Max);
public record GroupDef(string Key, string Name, Range[] Ranges);

[Generator(LanguageNames.CSharp)]
public class GroupsGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        var jsFile = context.AdditionalFiles.FirstOrDefault(f => Path.GetFileName(f.Path) == "groups.js");

        if (jsFile == null)
        {
            context.ReportDiagnostic(Diagnostic.Create("ISBN001", "Compiler", "groups.js not found",
                DiagnosticSeverity.Error, DiagnosticSeverity.Error, true, 1));
            return;
        }

        var text = File.ReadAllText(jsFile.Path);
        text = text[text.IndexOf('{')..];

        var source = RenderTemplate(text);

        context.AddSource("ISBN.Groups.g", SourceText.From(source, Encoding.UTF8));
    }

    public static string RenderTemplate(string json)
    {
        var data = JObject.Parse(json);
        var groups = new ConcurrentDictionary<string, ConcurrentDictionary<char, ConcurrentDictionary<string, GroupDef>>>();

        foreach (var prop in data.Properties())
        {
            if (prop?.Value is JObject obj &&
                obj.Value<string>("name") is string name &&
                obj.Property("ranges") is JProperty ranges &&
                ranges?.Value is JArray rangeValues)
            {
                var result = new List<Range>();
                foreach (var range in rangeValues)
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

                groups.GetOrAdd(prefix, _ => new())
                    .GetOrAdd(groupFirstDigit, _ => new())
                    [group] = new GroupDef(group, name, result.ToArray());
            }
        }

        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName), "Template.sbntxt");
        Template template;

        if (File.Exists(path))
        {
            template = Template.Parse(File.ReadAllText(path));
        }
        else
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GroupsGenerator.Template.sbntxt");
            using var reader = new StreamReader(stream);
            template = Template.Parse(reader.ReadToEnd());
        }

        var output = template.Render(new { map = groups }, member => member.Name);

        return output;
    }
}
