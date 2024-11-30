using Talkie.Handlers;
using Talkie.Sequences;

namespace Talkie.Pipelines.Handling;

public sealed class ParallelSignalHandlingPipelineSchedulerFactory : ISignalHandlingPipelineProcessorFactory
{
    public static readonly ParallelSignalHandlingPipelineSchedulerFactory Instance = new();

    private ParallelSignalHandlingPipelineSchedulerFactory() { }

    public ISignalHandlingPipelineProcessor Create(FrozenSequence<ISignalHandler> handlers)
    {
        return new ParallelSignalHandlingPipelineProcessor(handlers);
    }
}
