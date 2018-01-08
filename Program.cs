using System;

namespace CsAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            //WaterfallTest waterfallTest = new WaterfallTest();
            //waterfallTest.DoTest();
            //WhilstTest whilstTest = new WhilstTest();
            //whilstTest.DoTest();
            EachTest eachTest = new EachTest();
            eachTest.DoTest();
            Console.ReadKey();
        }

    }
}
