using System;
using System.Threading.Tasks;

namespace DotNetExtensions
{
    public static partial class TaskExtensions
    {
        public static async void ToVoid(this Task task)
        {
            await task;
        }
    }
}
