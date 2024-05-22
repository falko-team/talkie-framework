namespace Talkie.Controllers;

/// <summary>
/// Represents a builder for creating a controller creator.
/// </summary>
public interface IControllerCreatorBuilder
{
    /// <summary>
    /// Adds a controller factory to the builder that creates controllers of the specified type and context.
    /// </summary>
    /// <param name="factory">The factory to add.</param>
    /// <typeparam name="TController">The type of the controller.</typeparam>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <returns>The current builder.</returns>
    IControllerCreatorBuilder Add<TController, TContext>(Func<TContext, TController> factory)
        where TController : class, IController<TContext>
        where TContext : notnull;

    /// <summary>
    /// Creates a controller creator from the builder.
    /// </summary>
    /// <returns>The controller creator.</returns>
    IControllerCreator Build();
}
