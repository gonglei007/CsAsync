using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsAsync
{
    public class TaskCallback
    {
        private Action<Exception> callback = null;

        public TaskCallback(Action<Exception> callback) {
            this.callback = callback;
        }

        public void DoCallback(Exception e)
        {
            this.callback(e);
        }
    }
}
