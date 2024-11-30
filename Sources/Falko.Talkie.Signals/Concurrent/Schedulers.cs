using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;

namespace Talkie.Concurrent;

public static partial class Schedulers
{
    public static readonly HandlingSchedulers Handling = default;

    public static readonly InterceptingSchedulers Intercepting = default;
}

public readonly struct HandlingSchedulers;

public readonly struct InterceptingSchedulers;

public static partial class HandlingSchedulersExtensions
{
    public static ISignalHandlingPipelineProcessorFactory Current(this HandlingSchedulers _)
    {
        throw new NotImplementedException();
    }

    public static ISignalHandlingPipelineProcessorFactory Random(this HandlingSchedulers _)
    {
        throw new NotImplementedException();
    }

    public static ISignalHandlingPipelineProcessorFactory Parallel(this HandlingSchedulers _)
    {
        return ParallelSignalHandlingPipelineSchedulerFactory.Instance;
    }
}

public static partial class InterceptingSchedulersExtensions
{
    public static ISignalInterceptingPipelineProcessorFactory Current(this InterceptingSchedulers _)
    {
        throw new NotImplementedException();
    }

    public static ISignalInterceptingPipelineProcessorFactory Random(this InterceptingSchedulers _)
    {
        throw new NotImplementedException();
    }
}
