// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Interfaces.Commands;

namespace BB84.Notifications.Commands;

/// <summary>
/// Provides the cancellation state and execution pipeline shared by the asynchronous commands.
/// </summary>
/// <remarks>
/// This class owns the <see cref="CancellationTokenSource"/> of the running operation, the
/// executing flag that suppresses re-entrant execution, and the cancel command. The cancel
/// command is created once per instance so that subscribers to its
/// <see cref="System.Windows.Input.ICommand.CanExecuteChanged"/> event survive, and it is
/// notified whenever the owning command's executability changes.
/// </remarks>
public abstract class AsyncActionCommandBase
{
  private readonly ActionCommand _cancelCommand;
  private CancellationTokenSource? _cts;

  /// <summary>
  /// Initializes a new instance of the <see cref="AsyncActionCommandBase"/> class.
  /// </summary>
  protected AsyncActionCommandBase()
    => _cancelCommand = new ActionCommand(Cancel, () => IsExecuting);

  /// <inheritdoc cref="IAsyncActionCommand.IsCancellationRequested"/>
  public bool IsCancellationRequested => _cts?.IsCancellationRequested ?? false;

  /// <inheritdoc cref="IAsyncActionCommand.CancelCommand"/>
  /// <remarks>
  /// The same instance is returned for the lifetime of this command, so handlers attached to
  /// its <see cref="System.Windows.Input.ICommand.CanExecuteChanged"/> event stay attached.
  /// </remarks>
  public IActionCommand CancelCommand => _cancelCommand;

  /// <inheritdoc cref="System.Windows.Input.ICommand.CanExecuteChanged"/>
  public event EventHandler? CanExecuteChanged;

  /// <summary>
  /// Gets a value indicating whether an operation is currently running.
  /// </summary>
  protected bool IsExecuting { get; private set; }

  /// <inheritdoc cref="IAsyncActionCommand.Cancel"/>
  public void Cancel()
  {
    _cts?.Cancel();
    RaiseCanExecuteChanged();
  }

  /// <inheritdoc cref="IAsyncActionCommand.RaiseCanExecuteChanged"/>
  /// <remarks>
  /// Also notifies <see cref="CancelCommand"/>, whose executability tracks this command's.
  /// </remarks>
  public void RaiseCanExecuteChanged()
  {
    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    _cancelCommand.RaiseCanExecuteChanged();
  }

  /// <summary>
  /// Runs the supplied operation, tracking the executing state and exposing a cancellable token.
  /// </summary>
  /// <remarks>
  /// The token handed to <paramref name="body"/> is linked to <paramref name="cancellationToken"/>,
  /// so the operation is cancelled either by the caller's token or by <see cref="Cancel"/>.
  /// </remarks>
  /// <param name="body">The operation to run.</param>
  /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
  /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
  protected async Task ExecuteCoreAsync(Func<CancellationToken, Task> body, CancellationToken cancellationToken)
  {
    using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
    _cts = cts;

    try
    {
      IsExecuting = true;
      RaiseCanExecuteChanged();
      await body(cts.Token);
    }
    finally
    {
      IsExecuting = false;
      _cts = null;
      RaiseCanExecuteChanged();
    }
  }
}
