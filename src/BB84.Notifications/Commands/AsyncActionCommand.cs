using BB84.Notifications.Interfaces.Commands;

namespace BB84.Notifications.Commands;

/// <summary>
/// The async action command class.
/// </summary>
/// <param name="execute">The task to execute.</param>
/// <param name="canExecute">The condition to execute.</param>
public sealed class AsyncActionCommand(Func<Task> execute, Func<bool>? canExecute) : IAsyncActionCommand
{
  private bool _isExecuting;

  /// <summary>
  /// Initializes a new instance of the <see cref="AsyncActionCommand"/> class that can always execute.
  /// </summary>
  /// <param name="execute">The task to execute.</param>
  public AsyncActionCommand(Func<Task> execute) : this(execute, null)
  { }

  /// <inheritdoc/>
  public event EventHandler? CanExecuteChanged;

  /// <inheritdoc/>
  public bool CanExecute()
    => !_isExecuting && (canExecute?.Invoke() ?? true);

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
    => CanExecute();

  /// <inheritdoc/>
  public void Execute(object? parameter)
    => ExecuteAsync().Wait();

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
public sealed class AsyncActionCommand<T>(Func<T, Task> execute, Func<T, bool>? canExecute) : IAsyncActionCommand<T>
{
  private bool _isExecuting;

  /// <summary>
  /// Initializes a new instance of <see cref="AsyncActionCommand{T}"/> class that can always execute.
  /// </summary>
  /// <param name="execute">The task to execute.</param>
  public AsyncActionCommand(Func<T, Task> execute) : this(execute, null)
  { }

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
    => ExecuteAsync((T)parameter!).Wait();

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
