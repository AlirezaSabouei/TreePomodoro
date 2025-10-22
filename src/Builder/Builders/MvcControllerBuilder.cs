namespace Builder.Builders;

public class MvcControllerBuilder : BuilderBase
{
    protected override string ProjectPath { get; } = GlobalPathes.UiProjectPath;
    protected override string MainFolder { get; } = "Controllers";
    protected override string TemplatePath { get; } = "Templates/ControllerTemplate.cs";
    protected override string FilePostFix { get; } = "Controller";
    
    public override void Generate(string entityName, string pluralEntityName)
    {
        var directoryPath = Path.Combine(ProjectPath, MainFolder);
        Directory.CreateDirectory(directoryPath);
        var template = File.ReadAllText(TemplatePath);
        var content = template.Replace("{{EntityName}}", entityName).Replace("{{EntityPluralName}}", pluralEntityName);
        var filePath = Path.Combine(directoryPath, $"{entityName}{FilePostFix}.cs");
        File.WriteAllText(filePath, content);
        Console.WriteLine($"Created {filePath}");
    }
}