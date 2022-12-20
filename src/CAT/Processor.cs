using CAT.Analysers.Assemblies;
using CAT.Analysers.Methods;
using CAT.Processors;
using CAT.TreeStructure;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CAT;

public static class Processor
{
    public static string AnaliseAndParse(string dllPath)
    {
        List<MethodInformation> methods = Analise(dllPath);
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };

        return JsonSerializer.Serialize(methods, options);
    }

    public static List<MethodInformation> Analise(string dllPath)
    {
        List<Info> assemblies = AssemblyAnaliser.Getdependencies(dllPath);
        Console.WriteLine(string.Join(Environment.NewLine, assemblies.Select(a => a.Name + "; level:" + a.Level)));

        var methods = MethodsAnalyser.GetMethodsAnditsCalls(dllPath);
        var a = MethodProcessor.FindFirstAncestor(methods, "CAT.Test.Util.ClassToAnalize::MyMethod2()");

        return methods;
    }
}
