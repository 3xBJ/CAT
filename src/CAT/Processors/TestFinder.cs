using CAT.Analysers.Methods;

namespace CAT.Processors
{
    internal static class TestFinder
    {
        public static HashSet<string> ThatCallMethod(List<MethodInformation> methodsInfo, string fullName)
        {
            MethodInformation? method = methodsInfo.Find(m => m.Equals(fullName));
            if(method != null)
            {
                HashSet<string> originalCalls = new();
                FindOriginalCall(method, originalCalls);
                return originalCalls;
            }

            return new HashSet<string>();
            //return an object with a message and the list
        }

        private static void FindOriginalCall(MethodInformation method, HashSet<string> originalCalls)
        {
            foreach(var ancestor in method.CalledBy)
            {
                if(ancestor.CalledBy.Count == 0)
                {
                    originalCalls.Add(ancestor.FullName);
                }

                FindOriginalCall(ancestor, originalCalls);
            }
        }
    }
}
