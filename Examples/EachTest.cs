using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace CsAsync
{
    class EachTest
    {
        // Each测试案例
        public static void DoTest()
        {
            Console.WriteLine("== Test Each ==");

            string[] testString = { "item1", "item2", "item3" };
            Each each = new Each(
                testString,
                // 执行
                (object item, Action<Exception> callback) =>
                {
                    TestUtil.DoTask(item.ToString(), 1000, (Exception e) =>
                    {
                        if (item.ToString() == "item2")
                        {
                            callback(new Exception("Exception test!" + item));
                        }
                        else
                        {
                            callback(null);
                        }
                    });
                },
                // 结果
                (Exception e) =>
                {
                    if (e != null)
                    {
                        Console.WriteLine(e);
                    }
                    Console.WriteLine("Each: All done!");
                });
        }
    }
}
