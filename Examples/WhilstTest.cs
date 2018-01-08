using System;
using System.Collections.Generic;
using System.Timers;

namespace CsAsync
{
    class WhilstTest
    {

        /// <summary>
        /// 延迟几秒触发回调，触发任务。
        /// </summary>
        /// <param name="e"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public void DoTask(string strParam, Action<Exception> callback)
        {
            Console.WriteLine("Task-Begin");
            Timer timer = new Timer(1000);
            timer.Enabled = true;
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(
                (object source, ElapsedEventArgs eea) =>
                {
                    timer.Stop();
                    Console.WriteLine(strParam);
                    callback(null);
                }
            );
            Console.WriteLine("Task-End");
        }

        // Whilst测试案例
        public void DoTest()
        {
            string[] testString = { "item1", "item2", "item3"};
            int i = 0;
            Whilst whilst = new Whilst(
                // 测试
                ()=>{
                    return i < testString.Length;
                },
                // 执行
                (Action<Exception> callback) => {
                    string item = testString[i];
                    DoTask(item, (Exception e)=>{
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
