using System;
using System.Collections.Generic;
using System.Timers;

namespace CsAsync
{
    class WaterfallTest
    {

        /// <summary>
        /// 延迟几秒触发回调，触发下一个任务。
        /// </summary>
        /// <param name="e"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public void Task1(Exception e, Waterfall.CallbackDelegate callback)
        {
            Console.WriteLine("Task1-Begin");
            Timer timer = new Timer(1000);
            timer.Enabled = true;
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(
                (object source, ElapsedEventArgs eea) =>
                {
                    timer.Stop();
                    Console.WriteLine("Task1-Response(1s)");
                    callback(null);
                }
            );
            Console.WriteLine("Task1-End");
        }
        public void Task2(Exception e, Waterfall.CallbackDelegate callback)
        {
            Console.WriteLine("Task2-Begin");
            Timer timer = new Timer(2000);
            timer.Enabled = true;
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(
                (object source, ElapsedEventArgs eea) =>
                {
                    timer.Stop();
                    Console.WriteLine("Task2-Response(2s)");
                    callback(new Exception("Task1-Err!"));
                }
            );
            Console.WriteLine("Task2-End");
        }

        public void DoTest()
        {
            Waterfall waterfall = new Waterfall();
            waterfall.AddTask(Task1);
            waterfall.AddTask(Task2);
            waterfall.Start((Exception e) =>
            {
                Console.WriteLine("All done! -> " + e.ToString());
            });
        }
    }
}
