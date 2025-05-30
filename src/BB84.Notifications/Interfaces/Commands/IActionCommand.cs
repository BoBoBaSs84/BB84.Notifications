// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.Windows.Input;

namespace BB84.Notifications.Interfaces.Commands;

/// <summary>
/// Represents a command that can be executed based on a dynamic condition and provides functionality to
/// notify changes in its execution state.
/// </summary>
/// <remarks>
/// This interface extends <see cref="ICommand"/> by adding methods to explicitly check whether the command
/// can execute  and to raise notifications when the execution state changes. It is commonly used in scenarios
/// where commands are bound  to UI elements and their availability needs to be dynamically updated.
/// </remarks>
public interface IActionCommand : ICommand
{
  /// <summary>
  /// Determines whether the current operation can be executed.
  /// </summary>
  /// <returns>
  /// <see langword="true"/> if the operation can be executed; otherwise, <see langword="false"/>.
  /// </returns>
  bool CanExecute();

  /// <summary>
  /// Executes the associated action if the current conditions allow execution.
  /// </summary>
  /// <remarks>
  /// This method performs the primary action associated with. The specific behavior of the
  /// operation depends on the implementation. Ensure that any required preconditions are
  /// met before calling this method.
  /// </remarks>
  void Execute();

  /// <summary>
  /// Notifies subscribers that the ability to execute the command has changed.
  /// </summary>
  /// <remarks>
  /// This method should be called when the conditions that determine whether the command
  /// can execute have changed. It triggers the <see cref="ICommand.CanExecuteChanged"/>
  /// event, allowing UI  elements bound to the command to update their enabled state.
  /// </remarks>
  void RaiseCanExecuteChanged();
}

/// <summary>
/// Represents a command that can be executed with a parameter of type <typeparamref name="T"/>.
/// </summary>
/// <remarks>
/// This interface extends <see cref="ICommand"/> to provide strongly-typed methods for determining
/// whether the command can execute and for executing the command. It is commonly used in scenarios
/// where commands are bound to UI elements or invoked programmatically with specific data.
/// </remarks>
/// <typeparam name="T">The type of the parameter used by the command.</typeparam>
public interface IActionCommand<T> : ICommand
{
  /// <summary>
  /// Determines whether the command can execute in its current state with the specified parameter.
  /// </summary>
  /// <param name="parameter">
  /// The parameter to evaluate. This can be used to provide context or input for determining
  /// whether the command is executable.
  /// </param>
  /// <returns>
  /// <see langword="true"/> if the command can execute with the specified parameter; otherwise,
  /// <see langword="false"/>.
  /// </returns>
  bool CanExecute(T parameter);

  /// <summary>
  /// Executes the specified action if the associated condition is met.
  /// </summary>
  /// <remarks>
  /// This method performs the primary action associated with. The specific behavior of the
  /// operation depends on the implementation. Ensure that any required preconditions are
  /// met before calling this method.
  /// </remarks>
  /// <param name="parameter">
  /// The parameter to pass to the action. This can be null if the action supports null parameters.
  /// </param>
  void Execute(T parameter);

  /// <summary>
  /// Notifies subscribers that the ability to execute the command has changed.
  /// </summary>
  /// <remarks>
  /// This method should be called when the conditions that determine whether the command
  /// can execute have changed. It triggers the <see cref="ICommand.CanExecuteChanged"/>
  /// event, allowing UI  elements bound to the command to update their enabled state.
  /// </remarks>
  void RaiseCanExecuteChanged();
}
