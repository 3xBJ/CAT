using CAT.Analysers.Methods;

namespace CAT.Processors
{
    internal static class MethodProcessor
    {
        public static void Process(List<MethodInformation> methodsInfo)
        {
            IEnumerable<string> methodsNeverCalled = methodsInfo.Where(m => m.CalledBy.Count == 0).Select(m => m.FullName);

        }


    }
}
