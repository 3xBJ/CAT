using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("CAT.Test")]

namespace CAT.Analysers.Methods
{
    internal static class MethodsAnalyser
    {
        //following ecma documentation (link:)
        //CIL has 3 call instructions that are used to transfer argumentos values to a destination method
        //call : Used when the destination address is fixed at the time the CIL is linked
        //calli: Usen then the destination adrres is calculated at run time
        //callvirt: Uses the call of an object, know only at run time, to determine the method to be called
        private static readonly HashSet<string> callInstructions = new() { "call", "calli", "callvirt" };

        //newObj is constructor call

        internal static List<MethodInformation> GetMethodsAnditsCalls(string dllPath)
        {
            List<MethodInformation> methodsInfo = new();
            List<MethodInformation> methodsWhithoutCallAdded = new();
            AssemblyDefinition asm = AssemblyDefinition.ReadAssembly(dllPath);
            IEnumerable<MethodDefinition>? allMethods = asm.Modules.SelectMany(mod => mod.Types)
                                                                   .SelectMany(type => type.Methods)
                                                                   .Where(method => method.HasBody);

            List<MethodInformation> methods = new();

            foreach (MethodDefinition method in allMethods)
            {
                List<MethodInformation> calledMethods = ReturnCalledMethods(methodsWhithoutCallAdded, methods, method);

                MethodInformation? methodInfo = GetMethodInstance(methodsWhithoutCallAdded, method, calledMethods);

                methodsInfo.Add(methodInfo);
            }

            return methodsInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remaks>
        /// can hapen:
        ///  * The method was never called before
        ///  
        ///  * The method has already been called
        /// </remaks>
        private static List<MethodInformation> ReturnCalledMethods(List<MethodInformation> methodsWhithoutCallAdded, List<MethodInformation> methods, MethodDefinition method)
        {
            List<MethodInformation> calledMethods = new();

            foreach (Instruction instruction in method.Body.Instructions)
            {
                string instructionCode = instruction.ToString();

                if (callInstructions.Any(instructionCode.Contains))
                {
                    string methodFullName = instructionCode.Split().Last();
                    if (methodFullName.Split("::").Length < 2)
                    {
                        //someone is using the callInstructions whithout a method
                        continue;
                    }

                    MethodInformation? methodInfo = methods.Where(m => m.Equals(methodFullName)).FirstOrDefault();

                    //prevents creating more than one instance for the same method
                    if (methodInfo is not null)
                    {
                        calledMethods.Add(methodInfo);
                    }
                    else
                    {
                        var seila = new MethodInformation(methodFullName);
                        calledMethods.Add(seila);
                        methodsWhithoutCallAdded.Add(seila);
                    }
                }
            }

            return calledMethods;
        }

        private static MethodInformation GetMethodInstance(List<MethodInformation> methodsWhithoutCallAdded, MethodDefinition method, List<MethodInformation> calledMethods)
        {
            //if the method we are processing already has an instance due
            //to been called from a method already analised
            string fullName = method.FullName.Split(' ').Last();
            var methodInfo = methodsWhithoutCallAdded.FirstOrDefault(m => m.Equals(fullName));
            if (methodInfo is not null)
            {
                methodInfo.CalledMethods.AddRange(calledMethods);
            }
            else
            {
                methodInfo = new MethodInformation(fullName, calledMethods);
            }

            //TODO: try to do this when listing the methodscalled
            foreach(var methodCalled in calledMethods)
            {
                methodCalled.CalledBy.Add(methodInfo);
            }

            return methodInfo;
        }
    }
}
