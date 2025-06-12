namespace Falko.Talkie.Concurrent;

public static partial class TaskExtensions
{
    /// <summary>
    /// Converts the specified <see cref="Task"/> to a <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="task">The task to convert.</param>
    /// <returns>The converted <see cref="ValueTask"/>.</returns>
    public static ValueTask AsValueTask(this Task task) => new(task);
}
