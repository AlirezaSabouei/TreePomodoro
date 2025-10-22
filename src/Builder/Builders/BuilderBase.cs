namespace Builder.Builders;

public abstract class BuilderBase
{
    protected abstract string ProjectPath { get; }

    protected abstract string MainFolder { get; }
    protected abstract string TemplatePath { get; }
    protected abstract string FilePostFix { get; }

    public virtual void Generate(string entityName, string pluralEntityName)
    {
        var directoryPath = Path.Combine(ProjectPath, MainFolder, pluralEntityName);
        Directory.CreateDirectory(directoryPath);
        var template = File.ReadAllText(TemplatePath);
        var content = template.Replace("{{EntityName}}", entityName).Replace("{{EntityPluralName}}", pluralEntityName);
        var filePath = Path.Combine(directoryPath, $"{entityName}{FilePostFix}.cs");
        File.WriteAllText(filePath, content);
        Console.WriteLine($"Created {filePath}");
    }
}