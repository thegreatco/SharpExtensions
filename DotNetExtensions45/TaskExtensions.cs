using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetExtensions
{
    public static class TaskExtensions
    {
        public static async Task WithTimeout(this Task task, long timeout)
        {
            await WithTimeout(task, TimeSpan.FromMilliseconds(timeout));
        }

        public static async Task WithTimeout(this Task task, TimeSpan timeout)
        {
            if (task != await Task.WhenAny(task, Task.Delay(timeout)))
                throw new TimeoutException();

            await task;
        }

        public static async void ToVoid(this Task task)
        {
            await task;
        }
    }
}
