using System.Diagnostics.CodeAnalysis;
using Talkie.Interceptors;
using Talkie.Sequences;

namespace Talkie.Pipelines.Intercepting;

public static class SignalInterceptingPipelineFactory
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static ISignalInterceptingPipeline Create(IReadOnlySequence<ISignalInterceptor> interceptors)
    {
        return interceptors.Count > 0
            ? new SignalInterceptingPipeline(interceptors)
            : EmptySignalInterceptingPipeline.Instance;
    }
}
