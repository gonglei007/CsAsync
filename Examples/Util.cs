using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CsAsync
{
    class Util
    {
        public static void DoTask(string data, int ms, Action<Exception> callback){
            Console.WriteLine("[Task]-Start");
            Timer timer = new Timer(ms);
            timer.Enabled = true;
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(
                (object source, ElapsedEventArgs eea) =>
                {
                    timer.Stop();
                    Console.WriteLine(string.Format("[Task]-Doing:{0}\t({1}ms)", data, ms));
                    Console.WriteLine("[Task]-Finish");
                    callback(null);
                }
            );
        }
    }
}
