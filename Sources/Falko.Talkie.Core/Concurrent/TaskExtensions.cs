using System.Runtime.CompilerServices;

namespace Talkie.Concurrent;

public static partial class TaskExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTask AsValueTask(this Task task) => new(task);
}
