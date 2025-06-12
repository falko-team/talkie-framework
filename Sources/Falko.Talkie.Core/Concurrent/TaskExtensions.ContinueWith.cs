namespace Falko.Talkie.Concurrent;

public static partial class TaskExtensions
{
    private const TaskContinuationOptions TaskFaultOptions =
        TaskContinuationOptions.ExecuteSynchronously |
        TaskContinuationOptions.OnlyOnFaulted;

    private const TaskContinuationOptions TaskSuccessOptions =
        TaskContinuationOptions.ExecuteSynchronously |
        TaskContinuationOptions.OnlyOnRanToCompletion;

    public static Task<TOut> InterceptOnSuccess<TIn, TOut>(this Task<TIn> task, Func<TIn, TOut> @do)
    {
        return task.ContinueWith(context => @do(context.Result), TaskSuccessOptions);
    }

    public static Task<TOut> InterceptOnSuccess<TIn, TOut>(this Task<TIn> task, Func<TOut> @do)
    {
        return task.ContinueWith(_ => @do(), TaskSuccessOptions);
    }

    public static Task<TOut> InterceptOnSuccess<TOut>(this Task task, Func<TOut> @do)
    {
        return task.ContinueWith(_ => @do(), TaskSuccessOptions);
    }

    public static Task<TOut> InterceptOnFault<TOut>(this Task task, Func<AggregateException?, TOut> @do)
    {
        return task.ContinueWith(context => @do(context.Exception), TaskFaultOptions);
    }

    public static Task<TOut> InterceptOnFault<TOut>(this Task task, Func<TOut> @do)
    {
        return task.ContinueWith(_ => @do(), TaskFaultOptions);
    }

    public static Task HandleOnSuccess<TIn>(this Task<TIn> task, Action<TIn> @do)
    {
        return task.ContinueWith(context => @do(context.Result), TaskSuccessOptions);
    }

    public static Task HandleOnSuccess(this Task task, Action @do)
    {
        return task.ContinueWith(_ => @do(), TaskSuccessOptions);
    }

    public static Task HandleOnFault(this Task task, Action<AggregateException?> @do)
    {
        return task.ContinueWith(context => @do(context.Exception), TaskFaultOptions);
    }

    public static Task HandleOnFault(this Task task, Action @do)
    {
        return task.ContinueWith(_ => @do(), TaskFaultOptions);
    }
}
