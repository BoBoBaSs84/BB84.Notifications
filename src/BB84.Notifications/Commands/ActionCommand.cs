// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Interfaces.Commands;

namespace BB84.Notifications.Commands;

/// <summary>
/// The action command class.
/// </summary>
/// <param name="execute">The action to execute.</param>
/// <param name="canExecute">The condition to execute.</param>
public sealed class ActionCommand(Action execute, Func<bool>? canExecute) : IActionCommand
{
  /// <summary>
  /// Initializes a new instance of the <see cref="ActionCommand"/> class that can always execute.
  /// </summary>
  /// <param name="execute">The action to execute.</param>
  public ActionCommand(Action execute) : this(execute, null)
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
/// The action command class.
/// </summary>
/// <remarks>
/// For all commands that need a parameter.
/// </remarks>
/// <typeparam name="T">The generic type to work with.</typeparam>
/// <param name="execute">The action to execute.</param>
/// <param name="canExecute">The condition to execute.</param>
public sealed class ActionCommand<T>(Action<T> execute, Func<T, bool>? canExecute) : IActionCommand<T>
{
  /// <summary>
  /// Initializes a new instance of <see cref="ActionCommand{T}"/> class that can always execute.
  /// </summary>
  /// <param name="execute">The action to execute.</param>
  public ActionCommand(Action<T> execute) : this(execute, null)
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
