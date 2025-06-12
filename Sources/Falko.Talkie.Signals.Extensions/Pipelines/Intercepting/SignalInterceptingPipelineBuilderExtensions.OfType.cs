using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<TTo> OfType<TFrom, TTo>(this ISignalInterceptingPipelineBuilder<TFrom> builder)
        where TFrom : Signal
        where TTo : TFrom
    {
        return new SignalInterceptingPipelineBuilder<TTo>(builder.InterceptorFactories);
    }

    public static ISignalInterceptingPipelineBuilder<T> OfType<T>(this ISignalInterceptingPipelineBuilder builder)
        where T : Signal
    {
        return new SignalInterceptingPipelineBuilder<T>(builder.InterceptorFactories);
    }
}
