using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsAsync
{
    class Each
    {
        private object[] items = null;
        private Action<object, Action<Exception>> iterator = null;
        private Action<Exception> callback = null;

        private int index = 0;

        public Each(object[] items, Action<object, Action<Exception>> iterator, Action<Exception> callback) {
            this.items = items;
            this.iterator = iterator;
            this.callback = callback;

            DoTask();
        }

        private void DoTask() {
            if (index >= items.Length)
            {
                callback(null);
            }
            else { 
                object  item = items[index];
                iterator(item, (Exception e) => {
                    if (e != null)
                    {
                        callback(e);
                    }
                    else {
                        index++;
                        DoTask();
                    }
                });
            }
        }

    }
}
