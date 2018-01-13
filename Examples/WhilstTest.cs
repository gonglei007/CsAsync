using System;
using System.Collections.Generic;
using System.Timers;

namespace CsAsync
{
    class WhilstTest
    {
        // Whilst测试案例
        public static void DoTest()
        {
            Console.WriteLine("== Test Whilst ==");
            string[] testString = { "item1", "item2", "item3" };
            int i = 0;
            Whilst whilst = new Whilst(
                // 测试
                ()=>{
                    return i < testString.Length;
                },
                // 执行
                (Action<Exception> callback) => {
                    string item = testString[i];
                    TestUtil.DoTask(item, 1000, (Exception e) =>
                    {
                        i++;
                        if (item == "item2")
                        {
                            callback(new Exception("Exception test!" + item));
                        }
                        else {
                            callback(null);
                        }
                    });
                },
                // 结果
                (Exception e)=>{
                    if (e != null)
                    {
                        Console.WriteLine(e);
                    }
                    Console.WriteLine("All done!" );
            });
        }
    }
}
