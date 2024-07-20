using System.Diagnostics.CodeAnalysis;
using Talkie.Interceptors;

namespace Talkie.Pipelines.Intercepting;

public static class SignalInterceptingPipelineFactory
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static ISignalInterceptingPipeline Create(IEnumerable<ISignalInterceptor> interceptors)
    {
        return interceptors.Any()
            ? new SignalInterceptingPipeline(interceptors)
            : EmptySignalInterceptingPipeline.Instance;
    }
}
