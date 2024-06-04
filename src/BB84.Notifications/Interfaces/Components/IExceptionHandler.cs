namespace BB84.Notifications.Interfaces.Components;

/// <summary>
/// The exception handler interface.
/// </summary>
public interface IExceptionHandler
{
  /// <summary>
  /// If an error occurs, it send the exception to an error handler.
  /// </summary>
  /// <param name="exception">The exception to handle.</param>
  void HandleError(Exception exception);
}
