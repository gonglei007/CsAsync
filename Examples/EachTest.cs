using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CsAsync
{
    class EachTest
    {
        /// <summary>
        /// 延迟几秒触发回调，触发任务。
        /// </summary>
        /// <param name="e"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public void DoTask(object objParam, Action<Exception> callback)
        {
            Console.WriteLine("Task-Begin");
            Timer timer = new Timer(1000);
            timer.Enabled = true;
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(
                (object source, ElapsedEventArgs eea) =>
                {
                    timer.Stop();
                    Console.WriteLine(objParam.ToString());
                    callback(null);
                }
            );
            Console.WriteLine("Task-End");
        }

        // Each测试案例
        public void DoTest()
        {
            string[] testString = { "item1", "item2", "item3" };
            Each each = new Each(
                testString,
                // 执行
                (object item, Action<Exception> callback) =>
                {
                    DoTask(item, (Exception e) =>
                    {
                        /*
                        if (item == "item2")
                        {
                            callback(new Exception("Exception test!" + item));
                        }
                        else
                         */
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
