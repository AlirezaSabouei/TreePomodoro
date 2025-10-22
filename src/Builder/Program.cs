using System.Reflection;
using Builder.Builders;
using Microsoft.CodeAnalysis.MSBuild;

namespace Builder;

internal class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
        {
            Console.WriteLine("Please provide an entity name.");
            return;
        }
        
        string entityName = args[0];
        string pluralEntityName = args.Length == 2 ? args[1] : $"{args[0]}s";
        
        // Load Domain project
        using var workspace = MSBuildWorkspace.Create();
        var solution = await workspace.OpenSolutionAsync(GlobalPathes.SolutionPath);
        var domainProject = solution.Projects.FirstOrDefault(p => p.Name == GlobalPathes.DomainProjectName);
        
        if (domainProject == null)
        {
            Console.WriteLine("Domain project not found!");
            return;
        }
        
        // Check if entity exists
        var entityExists = false;
        foreach (var doc in domainProject.Documents)
        {
            var tree = await doc.GetSyntaxTreeAsync();
            var root = await tree!.GetRootAsync();
            var classes = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>();
            if (classes.Any(c => c.Identifier.Text == entityName))
            {
                entityExists = true;
                break;
            }
        }
        
        if (!entityExists)
        {
            Console.WriteLine($"Entity '{entityName}' not found in Domain project.");
            return;
        }
        
        // Generate files
        
        // Get the current assembly
        var assembly = Assembly.GetExecutingAssembly();

        // Find all types assignable to MyClass (including MyClass itself)
        var types = assembly.GetTypes()
            .Where(t => typeof(BuilderBase).IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var type in types)
        {
            // Create an instance
            var instance = Activator.CreateInstance(type) as BuilderBase;

            // Call method X
            instance?.Generate(entityName, pluralEntityName);
        }
        
        Console.WriteLine("Files generated successfully!");
    }
}