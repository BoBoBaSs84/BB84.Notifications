using System.Windows.Input;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The relay command interface.
/// </summary>
public interface IRelayCommand : ICommand
{
  /// <summary>
  /// Notifies that the <see cref="ICommand.CanExecute(object?)"/> property has changed.
  /// </summary>
  void NotifyCanExecuteChanged();
}

/// <summary>
/// The relay command interface.
/// </summary>
/// <typeparam name="T">The type to work with.</typeparam>
public interface IRelayCommand<T> : IRelayCommand
{
  /// <summary>
  /// Defines the method that determines whether the command can execute in its current state.
  /// </summary>
  /// <param name="parameter">The data used by the command.</param>
  /// <returns>true if this command can be executed; otherwise, false.</returns>
  bool CanExecute(T parameter);

  /// <summary>
  /// Defines the method to be called when the command is invoked.
  /// </summary>
  /// <param name="parameter">The data used by the command.</param>
  void Execute(T parameter);
}
