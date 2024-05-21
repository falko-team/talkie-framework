namespace Falko.Talkie.Concurrent;

public static class TaskExtensions
{
    public static ValueTask AsValueTask(this Task task) => new(task);
}
