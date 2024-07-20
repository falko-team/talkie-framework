namespace Talkie.Piepelines2.Intercepting;

public interface ISignalInterceptingPipelineBuilder : IReadOnlySignalInterceptingPipelineBuilder
{
    ISignalInterceptingPipelineBuilder Intercept(ISignalInterceptorFactory interceptorFactory);
}
