using System.Windows.Input;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The async relay command interface.
/// </summary>
public interface IAsyncRelayCommand : ICommand
{
  /// <summary>
  /// Defines the method to be called when the command is invoked.
  /// </summary>
  /// <returns><see cref="Task"/></returns>
  Task ExecuteAsync();

  /// <summary>
  /// Defines the method that determines whether the command can execute in its current state.
  /// </summary>
  /// <returns>True if this command can be executed, otherwise false.</returns>
  bool CanExecute();

  /// <summary>
  /// Notifies that the <see cref="ICommand.CanExecuteChanged"/> property has changed.
  /// </summary>
  void RaiseCanExecuteChanged();
}

/// <summary>
/// The async relay command interface.
/// </summary>
/// <typeparam name="T">The generic type to wor with.</typeparam>
public interface IAsyncRelayCommand<T> : ICommand
{
  /// <summary>
  /// Defines the method to be called when the command is invoked.
  /// </summary>
  /// <param name="parameter">The data used by the command.</param>
  /// <returns><see cref="Task"/></returns>
  Task ExecuteAsync(T parameter);

  /// <summary>
  /// Defines the method that determines whether the command can execute in its current state.
  /// </summary>
  /// <param name="parameter">The data used by the command.</param>
  /// <returns>True if this command can be executed, otherwise false.</returns>
  bool CanExecute(T parameter);

  /// <summary>
  /// Notifies that the <see cref="ICommand.CanExecuteChanged"/> property has changed.
  /// </summary>
  void RaiseCanExecuteChanged();
}
