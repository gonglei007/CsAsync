using System;
using System.Collections.Generic;

namespace CsAsync
{

    /// <summary>
    /// 循环任务流。
    /// </summary>
    class Whilst
    {
        Func<bool> test;
        Action<Action<Exception>> fn;
        Action<Exception> callback;

        public Whilst(Func<bool> test, Action<Action<Exception>> fn, Action<Exception> callback)
        {
            if (test == null || fn == null || callback == null)
            {
                throw new Exception("Invalid paramters!");
            }
            this.test = test;
            this.fn = fn;
            this.callback = callback;

            DoTask();
        }

        /// <summary>
        /// 开始执行任务。
        /// </summary>
        private void DoTask()
        {
            if (this.test())
            {
                this.fn((Exception e) =>
                {
                    if (e == null)
                    {
                        DoTask();
                    }
                    else
                    {
                        callback(e);
                    }
                });
            }
            else
            {
                callback(null);
            }
        }

    }

}
