using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
/// The relay command class.
/// </summary>
/// <param name="execute">The action to execute.</param>
/// <param name="canExecute">The condition to execute.</param>
public sealed class RelayCommand(Action execute, Func<bool>? canExecute) : IRelayCommand
{
  /// <summary>
  /// Initializes a new instance of the <see cref="RelayCommand"/> class that can always execute.
  /// </summary>
  /// <param name="execute">The action to execute.</param>
  public RelayCommand(Action execute) : this(execute, null)
  { }

  /// <inheritdoc/>
  public event EventHandler? CanExecuteChanged;

  /// <inheritdoc/>
  public bool CanExecute(object? parameter)
    => CanExecute();

  /// <inheritdoc/>
  public bool CanExecute()
    => canExecute?.Invoke() ?? true;

  /// <inheritdoc/>
  public void Execute(object? parameter)
    => Execute();

  /// <inheritdoc/>
  public void Execute()
  {
    if (CanExecute())
    {
      execute.Invoke();
      RaiseCanExecuteChanged();
    }
  }

  /// <inheritdoc/>
  public void RaiseCanExecuteChanged()
    => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

/// <summary>
/// The relay command class.
/// </summary>
/// <remarks>
/// For all commands that need a parameter.
/// </remarks>
/// <typeparam name="T">The generic type to work with.</typeparam>
/// <param name="execute">The action to execute.</param>
/// <param name="canExecute">The condition to execute.</param>
public sealed class RelayCommand<T>(Action<T> execute, Func<T, bool>? canExecute) : IRelayCommand<T>
{
  /// <summary>
  /// Initializes a new instance of <see cref="RelayCommand{T}"/> class that can always execute.
  /// </summary>
  /// <param name="execute">The action to execute.</param>
  public RelayCommand(Action<T> execute) : this(execute, null)
  { }

  /// <inheritdoc/>
  public event EventHandler? CanExecuteChanged;

  /// <inheritdoc/>
  public bool CanExecute(T parameter)
   => canExecute?.Invoke(parameter) ?? true;

  /// <inheritdoc/>
  public bool CanExecute(object? parameter)
    => CanExecute((T)parameter!);

  /// <inheritdoc/>
  public void Execute(T parameter)
  {
    if (CanExecute(parameter))
    {
      execute.Invoke(parameter);
      RaiseCanExecuteChanged();
    }
  }

  /// <inheritdoc/>
  public void Execute(object? parameter)
    => Execute((T)parameter!);

  /// <inheritdoc/>
  public void RaiseCanExecuteChanged()
    => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
