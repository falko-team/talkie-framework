namespace Falko.Unibot.Controllers;

/// <summary>
/// Represents a controller creator that creates controllers.
/// </summary>
public interface IControllerCreator
{
    /// <summary>
    /// Creates a controller of the specified type with the specified context.
    /// </summary>
    /// <param name="context">The context to create the controller with.</param>
    /// <typeparam name="TController">The type of the controller to create.</typeparam>
    /// <typeparam name="TContext">The type of the context to create the controller with.</typeparam>
    /// <returns>The created controller.</returns>
    public TController Create<TController, TContext>(TContext context)
        where TController : class, IController<TContext>
        where TContext : notnull;
}
