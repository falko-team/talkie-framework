using System.Diagnostics.CodeAnalysis;
using Talkie.Handlers;
using Talkie.Pipelines.Intercepting;

namespace Talkie.Pipelines.Handling;

public static class SignalHandlingPipelineFactory
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static ISignalHandlingPipeline Create(IEnumerable<ISignalHandler> handlers,
        ISignalInterceptingPipeline interceptingPipeline)
    {
        using var enumerator = handlers.GetEnumerator();

        if (enumerator.MoveNext() is false)
        {
            return EmptySignalHandlingPipeline.Instance;
        }

        if (enumerator.MoveNext() is false)
        {
            return new SingleSignalHandlingPipeline(enumerator.Current, interceptingPipeline);
        }

        return new ManySignalHandlingPipeline(handlers, interceptingPipeline);
    }
}
