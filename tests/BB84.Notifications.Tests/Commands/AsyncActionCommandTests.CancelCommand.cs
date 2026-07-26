// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Commands;
using BB84.Notifications.Interfaces.Commands;

namespace BB84.Notifications.Tests.Commands;

public sealed partial class AsyncActionCommandTests
{
  [TestMethod]
  public void CancelCommandReturnsTheSameInstance()
  {
    AsyncActionCommand command = new(() => Task.CompletedTask);

    IActionCommand first = command.CancelCommand;
    IActionCommand second = command.CancelCommand;

    Assert.AreSame(first, second);
  }

  [TestMethod]
  public void CancelCommandGenericReturnsTheSameInstance()
  {
    AsyncActionCommand<int> command = new(p => Task.CompletedTask);

    IActionCommand first = command.CancelCommand;
    IActionCommand second = command.CancelCommand;

    Assert.AreSame(first, second);
  }

  [TestMethod]
  public async Task CancelCommandNotifiesSubscribersWhileExecuting()
  {
    TaskCompletionSource<bool> started = new();
    TaskCompletionSource<bool> release = new();
    AsyncActionCommand command = new(async ct =>
    {
      started.SetResult(true);
      await release.Task;
    });

    int notifications = 0;
    IActionCommand cancelCommand = command.CancelCommand;
    cancelCommand.CanExecuteChanged += (s, e) => notifications++;

    Task execution = command.ExecuteAsync();
    await started.Task;

    // A cancel button bound to CancelCommand has to be told it became enabled.
    Assert.IsTrue(notifications > 0);

    release.SetResult(true);
    await execution;
  }

  [TestMethod]
  public async Task CancelCommandCanExecuteTracksTheExecutionState()
  {
    TaskCompletionSource<bool> started = new();
    TaskCompletionSource<bool> release = new();
    AsyncActionCommand command = new(async ct =>
    {
      started.SetResult(true);
      await release.Task;
    });

    IActionCommand cancelCommand = command.CancelCommand;

    Assert.IsFalse(cancelCommand.CanExecute());

    Task execution = command.ExecuteAsync();
    await started.Task;

    Assert.IsTrue(cancelCommand.CanExecute());

    release.SetResult(true);
    await execution;

    Assert.IsFalse(cancelCommand.CanExecute());
  }

  [TestMethod]
  public async Task CancelCommandRetainedBeforeExecutionStillCancels()
  {
    TaskCompletionSource<bool> started = new();
    bool wasCancelled = false;
    AsyncActionCommand command = new(async ct =>
    {
      started.SetResult(true);
      try { await Task.Delay(5000, ct); }
      catch (OperationCanceledException) { wasCancelled = true; throw; }
    });

    // Retaining the reference up front is what a binding does.
    IActionCommand cancelCommand = command.CancelCommand;

    Task execution = command.ExecuteAsync();
    await started.Task;

    cancelCommand.Execute();

    try { await execution; } catch (OperationCanceledException) { }

    Assert.IsTrue(wasCancelled);
  }

  [TestMethod]
  public async Task CancelCommandGenericNotifiesSubscribersWhileExecuting()
  {
    TaskCompletionSource<bool> started = new();
    TaskCompletionSource<bool> release = new();
    AsyncActionCommand<int> command = new(async (p, ct) =>
    {
      started.SetResult(true);
      await release.Task;
    });

    int notifications = 0;
    IActionCommand cancelCommand = command.CancelCommand;
    cancelCommand.CanExecuteChanged += (s, e) => notifications++;

    Task execution = command.ExecuteAsync(1);
    await started.Task;

    Assert.IsTrue(notifications > 0);

    release.SetResult(true);
    await execution;
  }
}
