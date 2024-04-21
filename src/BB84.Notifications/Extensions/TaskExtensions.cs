using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Extensions;

/// <summary>
/// The task extensions class.
/// </summary>
public static class TaskExtensions
{
  /// <summary>
  /// Awaits the provided <paramref name="task"/>, uses the exception <paramref name="handler"/>
  /// if an <see cref="Exception"/> occured and returns <see cref="void"/>.
  /// </summary>
  /// <param name="task">The task to await.</param>
  /// <param name="handler">Sends the exception to an exception handler.</param>
  public static async void ToSaveVoid(this Task task, IExceptionHandler? handler = null)
  {
    try
    {
      await task;
    }
    catch (Exception ex)
    {
      handler?.Handle(ex);
    }
  }
}
