using CAT.Analysers.Methods;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CAT.Test
{
    public class MethodAnalyserTest
    {
        private List<MethodInformation> methodsToTest;

        [OneTimeSetUp]
        public void Setup()
        {
            List<MethodInformation> methods = MethodsAnalyser.GetMethodsAnditsCalls("CAT.Test");
            this.methodsToTest = methods.Where(m => m.Location.Contains("CAT.Test.Util")).ToList();
        }

        [Test, Order(1)]
        public void GetAllMethodsFromNameSpace()
        {
            Assert.AreEqual(7, methodsToTest.Count);
        }

        [Test, Order(2)]
        public void FirstLevelCall()
        {
            MethodInformation myMethod1 = methodsToTest.First(m => m.Name.Equals("MyMethod1()"));
            Assert.IsNotNull(myMethod1);
            Assert.AreEqual(1, myMethod1.CalledMethods.Count);
            Assert.AreEqual("MyMethod2()", myMethod1.CalledMethods[0].Name);
        }

        [Test, Order(3)]
        public void SecondLevelCall()
        {
            MethodInformation myMethod2 = methodsToTest.First(m => m.Name.Equals("MyMethod1()"))
                                                       .CalledMethods[0];

            Assert.AreEqual(1, myMethod2.CalledMethods.Count);
            Assert.AreEqual("MyMethod3()", myMethod2.CalledMethods[0].Name);
        }

        [Test, Order(4)]
        public void GetAllMethodsCalled()
        {
            MethodInformation myMethod3 = methodsToTest.First(m => m.Name.Equals("MyMethod1()"))
                                                       .CalledMethods[0]
                                                       .CalledMethods[0];

            Assert.AreEqual(2, myMethod3.CalledMethods.Count);
            Assert.AreEqual("MyMethod2()", myMethod3.CalledMethods[0].Name);
            Assert.AreEqual("CAT.Test.Util.MyClass", myMethod3.CalledMethods[0].Location);
            Assert.AreEqual("MyMethod1()", myMethod3.CalledMethods[1].Name);
            Assert.AreEqual("CAT.Test.Util.MyClass", myMethod3.CalledMethods[1].Location);
        }
    }
}