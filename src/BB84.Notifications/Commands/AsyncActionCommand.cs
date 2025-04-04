// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Extensions;
using BB84.Notifications.Interfaces.Commands;

namespace BB84.Notifications.Commands;

/// <summary>
/// The async action command class.
/// </summary>
/// <param name="execute">The task to execute.</param>
/// <param name="canExecute">The condition to execute.</param>
/// <param name="action">The action to invoke if an exception occurs.</param>
public sealed class AsyncActionCommand(Func<Task> execute, Func<bool>? canExecute = null, Action<Exception>? action = null) : IAsyncActionCommand
{
  private bool _isExecuting;

  /// <inheritdoc/>
  public event EventHandler? CanExecuteChanged;

  /// <inheritdoc/>
  public bool CanExecute()
    => CanExecute(null);

  /// <inheritdoc/>
  public async Task ExecuteAsync()
  {
    if (CanExecute())
    {
      try
      {
        _isExecuting = true;
        await execute();
      }
      finally
      {
        _isExecuting = false;
      }
    }

    RaiseCanExecuteChanged();
  }

  /// <inheritdoc/>
  public bool CanExecute(object? parameter)
    => !_isExecuting && (canExecute?.Invoke() ?? true);

  /// <inheritdoc/>
  public void Execute(object? parameter)
    => ExecuteAsync().FireAndForgetSafeAsync(action);

  /// <inheritdoc/>
  public void RaiseCanExecuteChanged()
    => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

/// <summary>
/// The async action command class.
/// </summary>
/// <remarks>
/// For all commands that need a parameter.
/// </remarks>
/// <typeparam name="T">The generic type to work with.</typeparam>
/// <param name="execute">The task to execute.</param>
/// <param name="canExecute">The condition to execute.</param>
/// <param name="action">The action to invoke if an exception occurs.</param>
public sealed class AsyncActionCommand<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null, Action<Exception>? action = null) : IAsyncActionCommand<T>
{
  private bool _isExecuting;

  /// <inheritdoc/>
  public event EventHandler? CanExecuteChanged;

  /// <inheritdoc/>
  public bool CanExecute(T parameter)
    => !_isExecuting && (canExecute?.Invoke(parameter) ?? true);

  /// <inheritdoc/>
  public bool CanExecute(object? parameter)
    => CanExecute((T)parameter!);

  /// <inheritdoc/>
  public void Execute(object? parameter)
    => ExecuteAsync((T)parameter!).FireAndForgetSafeAsync(action);

  /// <inheritdoc/>
  public async Task ExecuteAsync(T parameter)
  {
    if (CanExecute(parameter))
    {
      try
      {
        _isExecuting = true;
        await execute(parameter);
      }
      finally
      {
        _isExecuting = false;
      }
    }

    RaiseCanExecuteChanged();
  }

  /// <inheritdoc/>
  public void RaiseCanExecuteChanged()
    => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
