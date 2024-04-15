using System.Windows.Input;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The relay command interface.
/// </summary>
public interface IRelayCommand : ICommand
{
  /// <summary>
  /// Defines the method that determines whether the command can execute in its current state.
  /// </summary>
  /// <returns>True if this command can be executed, otherwise false.</returns>
  bool CanExecute();

  /// <summary>
  /// Defines the method to be called when the command is invoked.
  /// </summary>
  void Execute();

  /// <summary>
  /// Notifies that the <see cref="ICommand.CanExecuteChanged"/> property has changed.
  /// </summary>
  void RaiseCanExecuteChanged();
}

/// <summary>
/// The relay command interface.
/// </summary>
/// <typeparam name="T">The type to work with.</typeparam>
public interface IRelayCommand<T> : ICommand
{
  /// <summary>
  /// Defines the method that determines whether the command can execute in its current state.
  /// </summary>
  /// <param name="parameter">The data used by the command.</param>
  /// <returns>True if this command can be executed, otherwise false.</returns>
  bool CanExecute(T parameter);

  /// <summary>
  /// Defines the method to be called when the command is invoked.
  /// </summary>
  /// <param name="parameter">The data used by the command.</param>
  void Execute(T parameter);

  /// <summary>
  /// Notifies that the <see cref="ICommand.CanExecuteChanged"/> property has changed.
  /// </summary>
  void RaiseCanExecuteChanged();
}
