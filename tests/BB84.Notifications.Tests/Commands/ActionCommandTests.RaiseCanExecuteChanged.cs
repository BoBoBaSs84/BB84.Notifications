// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

public sealed partial class ActionCommandTests
{
  [TestMethod]
  public void RaiseCanExecuteChanged()
  {
    bool commandChanged = default;
    bool intCommandChanged = default;
    TestClass test = new();
    test.Command.CanExecuteChanged += (s, e) => commandChanged = true;
    test.IntCommand.CanExecuteChanged += (s, e) => intCommandChanged = true;

    test.Command.RaiseCanExecuteChanged();
    test.CondCommand.RaiseCanExecuteChanged();
    test.IntCommand.RaiseCanExecuteChanged();
    test.CondIntCommand.RaiseCanExecuteChanged();

    Assert.IsTrue(commandChanged);
    Assert.IsTrue(intCommandChanged);
  }
}
