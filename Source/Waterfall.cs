using System;
using System.Collections.Generic;

namespace CsAsync
{

    /// <summary>
    /// 瀑布执行任务流。
    /// </summary>
    public class Waterfall
    {
        Queue<Action<TaskCallback>> tasks = new Queue<Action<TaskCallback>>();
        TaskCallback                resultCallback;
        public Waterfall()
        {
        }

        /// <summary>
        /// 添加任务
        /// 任务方法定义：void TaskMethod(Exception e, CallbackDelegate callback){}
        /// </summary>
        /// <param name="task">任务Action</param>
        public void AddTask(Action<TaskCallback> task)
        {
            tasks.Enqueue(task);
        }

        /// <summary>
        /// 开始执行任务序列
        /// </summary>
        /// <param name="resultCallback">任务全部执行完成后触发的回掉</param>
        public void Start(Action<Exception> resultCallback)
        {
            TaskCallback taskCallback = new TaskCallback(resultCallback);
            this.resultCallback = taskCallback;

            if (tasks.Count == 0)
            {
                resultCallback(null);
                return;
            }
            DoTask();
        }

        /// <summary>
        /// 开始执行任务。
        /// </summary>
        private void DoTask() {
            Action<TaskCallback> task = tasks.Dequeue();
            TaskCallback taskCallback = new TaskCallback(OnCallback);
            task(taskCallback);
        }

        /// <summary>
        /// 当任务完成后，调用回调，会执行此方法。这个方法会在任务间传递。
        /// 如果没有异常，进入下一个任务。
        /// </summary>
        /// <param name="e"></param>
        private void OnCallback(Exception e) {
            if (tasks.Count == 0 || e != null)
            {
                this.resultCallback.DoCallback(e);
                return;
            }
            DoTask();
        }
    }

}
