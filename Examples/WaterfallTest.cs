using System;
using System.Collections.Generic;
using System.Timers;

namespace CsAsync
{
    class WaterfallTest
    {
        public static void DoTest()
        {
            Console.WriteLine("== Test Waterfall ==");
            Waterfall waterfall = new Waterfall();
            waterfall.AddTask((TaskCallback callback) =>
            {
                // 延迟几秒触发回调，触发下一个任务。
                TestUtil.DoTask("task1", 1000, (Exception e) =>
                {
                    callback.DoCallback(e);
                });
            });
            waterfall.AddTask((TaskCallback callback) =>
            {
                TestUtil.DoTask("task2", 2000, (Exception e) =>
                {
                    callback.DoCallback(e);
                });
            });
            waterfall.Start((Exception e) =>
            {
                Console.WriteLine("All done! -> " + ((e == null)? "No error" : e.ToString()));
            });
        }
    }
}
