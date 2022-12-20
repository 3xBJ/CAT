//using CAT.Analysers.Assemblies;
//using CAT.Analysers.Methods;
//using CAT.TreeStructure;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//Console.WriteLine("Hello, World!");

//string dllPath = "CAT.Test";

//List<Info> assemblies = AssemblyAnaliser.Getdependencies(dllPath);
//Console.WriteLine(string.Join(Environment.NewLine, assemblies.Select(a => a.Name + "; level:" + a.Level)));

//List<MethodInformation> methods = MethodsAnalyser.GetMethodsAnditsCalls(dllPath);

//JsonSerializerOptions options = new()
//{
//    ReferenceHandler = ReferenceHandler.Preserve,
//    WriteIndented = true
//};

//string json = JsonSerializer.Serialize(methods, options);

//Console.ReadLine();