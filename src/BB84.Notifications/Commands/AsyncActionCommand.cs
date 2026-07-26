// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Extensions;
using BB84.Notifications.Interfaces.Commands;

namespace BB84.Notifications.Commands;

/// <summary>
/// Represents an asynchronous command that can be executed and queried for its ability to execute.
/// </summary>
/// <remarks>
/// The command manages its own <see cref="CancellationTokenSource"/> internally. Call
/// <see cref="AsyncActionCommandBase.Cancel"/> (or execute
/// <see cref="AsyncActionCommandBase.CancelCommand"/>) to cancel a running operation.
/// </remarks>
public sealed class AsyncActionCommand : AsyncActionCommandBase, IAsyncActionCommand
{
  private readonly Func<CancellationToken, Task> _execute;
  private readonly Func<bool>? _canExecute;
  private readonly Action<Exception>? _action;

  /// <summary>
  /// Initializes a new instance of <see cref="AsyncActionCommand"/>.
  /// </summary>
  /// <param name="execute">The task to execute (without cancellation token).</param>
  /// <param name="canExecute">The condition to execute.</param>
  /// <param name="action">The action to invoke if an exception occurs.</param>
  public AsyncActionCommand(Func<Task> execute, Func<bool>? canExecute = null, Action<Exception>? action = null)
  {
    _execute = _ => execute();
    _canExecute = canExecute;
    _action = action;
  }

  /// <summary>
  /// Initializes a new instance of <see cref="AsyncActionCommand"/> with a cancellable execute delegate.
  /// </summary>
  /// <param name="execute">The task to execute, accepting a <see cref="CancellationToken"/>.</param>
  /// <param name="canExecute">The condition to execute.</param>
  /// <param name="action">The action to invoke if an exception occurs.</param>
  public AsyncActionCommand(Func<CancellationToken, Task> execute, Func<bool>? canExecute = null, Action<Exception>? action = null)
  {
    _execute = execute;
    _canExecute = canExecute;
    _action = action;
  }

  /// <inheritdoc/>
  public bool CanExecute()
    => CanExecute(null);

  /// <inheritdoc/>
  public bool CanExecute(object? parameter)
    => !IsExecuting && (_canExecute?.Invoke() ?? true);

  /// <inheritdoc/>
  public Task ExecuteAsync()
    => ExecuteAsync(CancellationToken.None);

  /// <inheritdoc/>
  public async Task ExecuteAsync(CancellationToken cancellationToken)
  {
    if (!CanExecute())
    {
      RaiseCanExecuteChanged();
      return;
    }

    await ExecuteCoreAsync(_execute, cancellationToken);
  }

  /// <inheritdoc/>
  public void Execute(object? parameter)
    => ExecuteAsync().FireAndForgetSafeAsync(_action);
}

/// <summary>
/// Represents an asynchronous command that can be executed with a parameter of type <typeparamref name="T"/>
/// and queried for its ability to execute.
/// </summary>
/// <remarks>
/// The command manages its own <see cref="CancellationTokenSource"/> internally. Call
/// <see cref="AsyncActionCommandBase.Cancel"/> (or execute
/// <see cref="AsyncActionCommandBase.CancelCommand"/>) to cancel a running operation.
/// </remarks>
/// <typeparam name="T">The generic type to work with.</typeparam>
public sealed class AsyncActionCommand<T> : AsyncActionCommandBase, IAsyncActionCommand<T>
{
  private readonly Func<T, CancellationToken, Task> _execute;
  private readonly Func<T, bool>? _canExecute;
  private readonly Action<Exception>? _action;

  /// <summary>
  /// Initializes a new instance of <see cref="AsyncActionCommand{T}"/>.
  /// </summary>
  /// <param name="execute">The task to execute (without cancellation token).</param>
  /// <param name="canExecute">The condition to execute.</param>
  /// <param name="action">The action to invoke if an exception occurs.</param>
  public AsyncActionCommand(Func<T, Task> execute, Func<T, bool>? canExecute = null, Action<Exception>? action = null)
  {
    _execute = (parameter, _) => execute(parameter);
    _canExecute = canExecute;
    _action = action;
  }

  /// <summary>
  /// Initializes a new instance of <see cref="AsyncActionCommand{T}"/> with a cancellable execute delegate.
  /// </summary>
  /// <param name="execute">The task to execute, accepting a parameter and a <see cref="CancellationToken"/>.</param>
  /// <param name="canExecute">The condition to execute.</param>
  /// <param name="action">The action to invoke if an exception occurs.</param>
  public AsyncActionCommand(Func<T, CancellationToken, Task> execute, Func<T, bool>? canExecute = null, Action<Exception>? action = null)
  {
    _execute = execute;
    _canExecute = canExecute;
    _action = action;
  }

  /// <inheritdoc/>
  public bool CanExecute(T parameter)
    => !IsExecuting && (_canExecute?.Invoke(parameter) ?? true);

  /// <inheritdoc/>
  public bool CanExecute(object? parameter)
    => CanExecute((T)parameter!);

  /// <inheritdoc/>
  public Task ExecuteAsync(T parameter)
    => ExecuteAsync(parameter, CancellationToken.None);

  /// <inheritdoc/>
  public async Task ExecuteAsync(T parameter, CancellationToken cancellationToken)
  {
    if (!CanExecute(parameter))
    {
      RaiseCanExecuteChanged();
      return;
    }

    await ExecuteCoreAsync(token => _execute(parameter, token), cancellationToken);
  }

  /// <inheritdoc/>
  public void Execute(object? parameter)
    => ExecuteAsync((T)parameter!).FireAndForgetSafeAsync(_action);
}
