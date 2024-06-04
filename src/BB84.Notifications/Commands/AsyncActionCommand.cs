using BB84.Notifications.Extensions;
using BB84.Notifications.Interfaces.Commands;
using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Commands;

/// <summary>
/// The async action command class.
/// </summary>
/// <param name="execute">The task to execute.</param>
/// <param name="canExecute">The condition to execute.</param>
/// <param name="handler">The exception handler to use.</param>
public sealed class AsyncActionCommand(Func<Task> execute, Func<bool>? canExecute = null, IExceptionHandler? handler = null) : IAsyncActionCommand
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
    => ExecuteAsync().FireAndForgetSafeAsync(handler);

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
/// <param name="handler">The exception handler to use.</param>
public sealed class AsyncActionCommand<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null, IExceptionHandler? handler = null) : IAsyncActionCommand<T>
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
    => ExecuteAsync((T)parameter!).FireAndForgetSafeAsync(handler);

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
