// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.Windows.Input;

namespace BB84.Notifications.Interfaces.Commands;

/// <summary>
/// The action command interface.
/// </summary>
public interface IActionCommand : ICommand
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
/// The action command interface.
/// </summary>
/// <typeparam name="T">The generic type to work with.</typeparam>
public interface IActionCommand<T> : ICommand
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
