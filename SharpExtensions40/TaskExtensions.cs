using System.Threading.Tasks;

namespace SharpExtensions
{
    public static partial class TaskExtensions
    {
        public static async void ToVoid(this Task task)
        {
            await task;
        }
    }
}
