using Falko.Unibot.Interceptors;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<T> WhereCommand<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        string name) where T : Signal
    {
        return builder.Where((signal, _) => true);
    }

    public static ISignalInterceptingPipelineBuilder<T> Where<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, CancellationToken, bool> where) where T : Signal
    {
        return builder.Intercept(new WhereSignalInterceptor<T>(where));
    }

    public static ISignalInterceptingPipelineBuilder<T> Where<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, bool> where) where T : Signal
    {
        return builder.Where((signal, _) => where(signal));
    }

    public static ISignalInterceptingPipelineBuilder Where(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, CancellationToken, bool> where)
    {
        return builder.Intercept(new WhereSignalInterceptor(where));
    }

    public static ISignalInterceptingPipelineBuilder Where(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, bool> where)
    {
        return builder.Where((signal, _) => where(signal));
    }
}
