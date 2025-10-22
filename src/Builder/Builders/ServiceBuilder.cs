namespace Builder.Builders;

public class ServiceBuilder : BuilderBase
{
    protected override string ProjectPath { get; } = GlobalPathes.BusinessProjectPath;
    protected override string MainFolder { get; } = "Services";
    protected override string TemplatePath { get; } = "Templates/ServiceTemplate.cs";
    protected override string FilePostFix { get; } = "Services";
}