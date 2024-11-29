namespace Talkie.Controllers;

/// <inheritdoc />
public sealed class ControllerCreator(IReadOnlyDictionary<Type, Func<object, object>> factories) : IControllerCreator
{
    public TController Create<TController, TContext>(TContext context)
        where TController : class, IController<TContext>
        where TContext : notnull
    {
        if (factories.TryGetValue(typeof(TController), out var factory) is false)
        {
            throw new NotSupportedException();
        }

        return factory(context) as TController ?? throw new NotSupportedException();
    }
}
