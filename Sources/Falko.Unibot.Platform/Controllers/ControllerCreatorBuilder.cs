using System.Collections.Frozen;

namespace Falko.Unibot.Controllers;

/// <inheritdoc />
public sealed class ControllerCreatorBuilder : IControllerCreatorBuilder
{
    private readonly Dictionary<Type, Func<object, object>> _factories = new();

    private ControllerCreatorBuilder() { }

    public IControllerCreatorBuilder Add<TController, TContext>(Func<TContext, TController> factory) where TController : class, IController<TContext> where TContext : notnull
    {
        _factories.Add(typeof(TController), context =>
        {
            if (context is TContext typedContext is false)
            {
                throw new NotSupportedException();
            }

            return factory(typedContext);
        });

        return this;
    }

    public IControllerCreator Build()
    {
        return new ControllerCreator(_factories.ToFrozenDictionary());
    }

    public static IControllerCreatorBuilder Create() => new ControllerCreatorBuilder();
}
