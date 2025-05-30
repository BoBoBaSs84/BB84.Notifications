// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.Windows.Input;

namespace BB84.Notifications.Interfaces.Commands;

/// <summary>
/// Represents an asynchronous command that can be executed and queried for its ability to execute.
/// </summary>
/// <remarks>
/// This interface extends <see cref="ICommand"/> to support asynchronous operations.
/// It provides methods for executing the command asynchronously, determining whether the command can execute,
/// and raising notifications when the ability to execute changes.
/// </remarks>
public interface IAsyncActionCommand : ICommand
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
/// Represents an asynchronous command that can be executed with a parameter of type <typeparamref name="T"/>.
/// </summary>
/// <remarks>
/// This interface extends <see cref="ICommand"/> to support asynchronous operations.
/// It provides methods to execute the command asynchronously, determine whether the command can be executed,
/// and notify changes to the command's ability to execute.
/// </remarks>
/// <typeparam name="T">The type of the parameter used by the command.</typeparam>
public interface IAsyncActionCommand<T> : ICommand
{
  /// <summary>
  /// Executes an asynchronous operation using the specified parameter.
  /// </summary>
  /// <remarks>
  /// This method performs an operation asynchronously and does not return a result.
  /// The caller is responsible for awaiting the returned <see cref="Task"/> to ensure the operation completes.
  /// </remarks>
  /// <param name="parameter">
  /// The input parameter of type <typeparamref name="T"/> that is required to perform the operation.
  /// </param>
  /// <returns>
  /// A <see cref="Task"/> representing the asynchronous operation.
  /// </returns>
  Task ExecuteAsync(T parameter);

  /// <summary>
  /// Determines whether the command can execute with the specified parameter.
  /// </summary>
  /// <param name="parameter">
  /// The parameter to evaluate. This can be used to determine whether the command is valid for execution.
  /// </param>
  /// <returns>
  /// <see langword="true"/> if the command can execute with the specified parameter; otherwise, <see langword="false"/>.
  /// </returns>
  bool CanExecute(T parameter);

  /// <summary>
  /// Notifies subscribers that the ability to execute the command may have changed.
  /// </summary>
  /// <remarks>
  /// This method should be called when the conditions determining whether the command can execute
  /// have changed. It triggers the <see cref="ICommand.CanExecuteChanged"/> event, allowing UI
  /// elements bound to the command to update their enabled state.
  /// </remarks>
  void RaiseCanExecuteChanged();
}
