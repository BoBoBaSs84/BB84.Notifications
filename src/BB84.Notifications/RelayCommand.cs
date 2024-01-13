using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
/// The relay command class.
/// </summary>
/// <remarks>
/// A command whose sole purpose is to relay its functionality to other
/// objects by invoking delegates. The default return value for the
/// <see cref="CanExecute(object?)"/> method is <see langword="true"/>.
/// </remarks>
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
    => canExecute is null || canExecute.Invoke();

  /// <inheritdoc/>
  public void Execute(object? parameter)
    => execute.Invoke();

  /// <inheritdoc/>
  public void NotifyCanExecuteChanged()
    => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

/// <summary>
/// The relay command class <typeparamref name="T"/>.
/// </summary>
/// <remarks>
/// A command whose sole purpose is to relay its functionality to other
/// objects by invoking delegates. The default return value for the
/// <see cref="CanExecute(T)"/> method is <see langword="true"/>.
/// </remarks>
/// <typeparam name="T">The type to wor with</typeparam>
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
   => canExecute is null || canExecute.Invoke(parameter);

  /// <inheritdoc/>
  public bool CanExecute(object? parameter)
    => CanExecute((T)parameter!);

  /// <inheritdoc/>
  public void Execute(T parameter)
    => execute.Invoke(parameter);

  /// <inheritdoc/>
  public void Execute(object? parameter)
    => Execute((T)parameter!);

  /// <inheritdoc/>
  public void NotifyCanExecuteChanged()
    => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
