namespace Talkie.Concurrent;

public static partial class TaskExtensions
{
    public static ValueTask AsValueTask(this Task task) => new(task);
}
