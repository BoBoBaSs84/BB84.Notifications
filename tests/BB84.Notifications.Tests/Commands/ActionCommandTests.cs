// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Commands;
using BB84.Notifications.Interfaces.Commands;

namespace BB84.Notifications.Tests;

[TestClass]
public sealed partial class ActionCommandTests
{
  private sealed class TestClass
  {
    private IActionCommand? _command;
    private IActionCommand? _condCommand;
    private IActionCommand<int>? _intCommand;
    private IActionCommand<int>? _condIntCommand;

    public TestClass()
    {
      CommandExecution = default;
      IntegerCommandExecution = default;
    }

    public IActionCommand Command
      => _command ??= new ActionCommand(() => CommandExecution++);

    public IActionCommand CondCommand
      => _condCommand ??= new ActionCommand(() => CommandExecution++, () => false);

    public IActionCommand<int> IntCommand
      => _intCommand ??= new ActionCommand<int>(x => IntegerCommandExecution = x);

    public IActionCommand<int> CondIntCommand
      => _condIntCommand ??= new ActionCommand<int>(x => IntegerCommandExecution = x, x => false);

    public int CommandExecution { get; private set; }

    public int IntegerCommandExecution { get; private set; }
  }
}
