// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Commands;
using BB84.Notifications.Interfaces.Commands;

namespace BB84.NotificationsTests.Commands;

[TestClass]
public sealed partial class AsyncActionCommandTests
{
  private sealed class TestClass
  {
    private IAsyncActionCommand? _command;
    private IAsyncActionCommand? _condCommand;
    private IAsyncActionCommand<int>? _intCommand;
    private IAsyncActionCommand<int>? _condIntCommand;

    public IAsyncActionCommand Command
      => _command ??= new AsyncActionCommand(CommandExecution);

    public IAsyncActionCommand CondCommand
      => _condCommand ??= new AsyncActionCommand(CommandExecution, () => false);

    public IAsyncActionCommand<int> IntCommand
      => _intCommand ??= new AsyncActionCommand<int>(IntegerCommandExecution);

    public IAsyncActionCommand<int> CondIntCommand
      => _condIntCommand ??= new AsyncActionCommand<int>(IntegerCommandExecution, x => false);

    public int Execution { get; private set; }

    public int IntegerExecution { get; private set; }

    private Task CommandExecution()
    {
      Execution++;
      return Task.CompletedTask;
    }

    private Task IntegerCommandExecution(int x)
    {
      IntegerExecution = x;
      return Task.CompletedTask;
    }
  }
}
