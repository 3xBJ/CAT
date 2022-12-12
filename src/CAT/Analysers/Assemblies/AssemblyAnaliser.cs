using CAT.TreeStructure;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("CAT.Test")]

namespace CAT.Analysers.Assemblies
{
    internal static class AssemblyAnaliser
    {
        internal static List<Info> Getdependencies(string dllPath)
        {
            Stack<Info> assembliesToanalize = new();
            assembliesToanalize.Push(new Info(dllPath, 0));

            //create empty stack push root assembly reference(s) onto stack
            //while (stack is not empty)
            //{ pop the top assembly reference display info about the popped item get all assembly references
            //  of the popped item foreach reference push the reference onto the stack, "right-to-left" }
            List<Info> assemblies = new();
            while (assembliesToanalize.Count > 0)
            {
                Info assemblyInfo = assembliesToanalize.Pop();

                if (assemblies.Contains(assemblyInfo))
                {
                    continue;
                }

                assemblies.Add(assemblyInfo);

                Assembly assembly = GetAssemblyByName(assemblyInfo.Name);
                AssemblyName[] dependencies = assembly.GetReferencedAssemblies();

                int level = assemblyInfo.Level + 1;
                foreach (AssemblyName name in dependencies)
                {
                    assembliesToanalize.Push(new Info(name.Name, level));
                }
            }

            return assemblies;
        }

        private static Assembly GetAssemblyByName(string name)
        {
            string dllName = name + ".dll";

            return File.Exists(dllName) ? Assembly.LoadFrom(dllName) :
                                          Assembly.Load(name);
        }
    }
}
