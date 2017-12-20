using System;
using System.Collections.Generic;

namespace CsAsync
{

    /// <summary>
    /// 瀑布执行任务流。
    /// </summary>
    class Waterfall
    {
        Queue<Action<Exception, CallbackDelegate>> tasks = new Queue<Action<Exception, CallbackDelegate>>();
        Action<Exception>           result;
        public delegate void CallbackDelegate(Exception e);
        public Waterfall()
        {
        }

        public void AddTask(Action<Exception, CallbackDelegate> task)
        {
            tasks.Enqueue(task);
        }

        public void Start(Action<Exception> result)
        {
            this.result = result;

            if (tasks.Count == 0)
            {
                result(null);
                return;
            }
            DoTask();
        }

        /// <summary>
        /// 开始执行任务。
        /// </summary>
        private void DoTask() {
            Action<Exception, CallbackDelegate> task = tasks.Dequeue();
            task(null, OnCallback);
        }

        /// <summary>
        /// 当任务完成后，调用回调，会执行此方法。这个方法会在任务间传递。
        /// 如果没有异常，进入下一个任务。
        /// </summary>
        /// <param name="e"></param>
        /// <param name="callback"></param>
        private void OnCallback(Exception e) {
            if (tasks.Count == 0 || e != null)
            {
                result(e);
                return;
            }
            DoTask();
        }
    }

}
