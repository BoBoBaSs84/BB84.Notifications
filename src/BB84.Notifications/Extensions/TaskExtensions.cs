// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
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
