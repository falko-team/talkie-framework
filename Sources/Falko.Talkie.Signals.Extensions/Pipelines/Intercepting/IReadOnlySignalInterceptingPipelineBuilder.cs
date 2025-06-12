using System.Collections.Immutable;

namespace Falko.Talkie.Pipelines.Intercepting;

public interface IReadOnlySignalInterceptingPipelineBuilder
{
    ImmutableStack<ISignalInterceptorFactory> InterceptorFactories { get; }

    ISignalInterceptingPipeline Build();
}
