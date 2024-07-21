using System.Diagnostics.CodeAnalysis;
using Talkie.Collections;
using Talkie.Interceptors;

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
