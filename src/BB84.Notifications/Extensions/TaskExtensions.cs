namespace BB84.Notifications.Extensions;

/// <summary>
/// The task extensions class.
/// </summary>
public static class TaskExtensions
{
  /// <summary>
  /// Awaits the provided <paramref name="task"/> and returns <see langword="void"/>.
  /// The <paramref name="action"/> can be used, if an <see cref="Exception"/> occurs.
  /// </summary>
  /// <param name="task">The task to await.</param>
  /// <param name="action">The action to invoke if an exception occurs.</param>
  public static async void FireAndForgetSafeAsync(this Task task, Action<Exception>? action = null)
  {
    try
    {
      await task.ConfigureAwait(false);
    }
    catch (Exception ex)
    {
      action?.Invoke(ex);
    }
  }
}
