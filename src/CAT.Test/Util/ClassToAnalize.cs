namespace CAT.Test.Util
{
    internal class ClassToAnalize
    {
        public void MyMethod1()
        {
            MyMethod2();
        }

        protected void MyMethod2()
        {
            MyMethod3();
        }

        private void MyMethod3()
        {
            MyClass myClass = new();
            myClass.MyMethod2();
            myClass.MyMethod1();
        }
    }

   internal class MyClass
    {
        public void MyMethod1()
        {

        }

        public void MyMethod2()
        {
            MyMethod1();
        }
    }
}
