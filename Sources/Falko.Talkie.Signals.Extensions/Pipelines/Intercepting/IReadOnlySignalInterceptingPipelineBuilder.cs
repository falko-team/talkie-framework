using Talkie.Pipelines.Intercepting;

namespace Talkie.Piepelines2.Intercepting;

public interface IReadOnlySignalInterceptingPipelineBuilder
{
    IEnumerable<ISignalInterceptorFactory> InterceptorFactories { get; }

    ISignalInterceptingPipeline Build();
}
