using System.Diagnostics.CodeAnalysis;
using Falko.Talkie.Interceptors;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Pipelines.Intercepting;

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
