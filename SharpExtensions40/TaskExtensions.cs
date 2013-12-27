using System;
using System.Threading.Tasks;

namespace DotNetExtensions
{
    public static class TaskExtensions
    {
        public static async void ToVoid(this Task task)
        {
            await task;
        }
    }
}
