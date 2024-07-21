namespace Talkie.Pipelines.Intercepting;

public interface ISignalInterceptingPipelineBuilder : IReadOnlySignalInterceptingPipelineBuilder
{
    ISignalInterceptingPipelineBuilder Intercept(ISignalInterceptorFactory interceptorFactory);
}
