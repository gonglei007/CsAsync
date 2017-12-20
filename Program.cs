using System;

namespace CsAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            WaterfallTest test = new WaterfallTest();
            test.DoTest();
            Console.ReadKey();
        }

    }
}
