using System;

namespace CsAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            //WaterfallTest waterfallTest = new WaterfallTest();
            //waterfallTest.DoTest();
            WhilstTest whilstTest = new WhilstTest();
            whilstTest.DoTest();
            Console.ReadKey();
        }

    }
}
