using System.Collections.Immutable;

namespace Talkie.Pipelines.Intercepting;

public interface IReadOnlySignalInterceptingPipelineBuilder
{
    ImmutableStack<ISignalInterceptorFactory> InterceptorFactories { get; }

    ISignalInterceptingPipeline Build();
}
