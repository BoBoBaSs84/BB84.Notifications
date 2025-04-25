// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

public sealed partial class ActionCommandTests
{
  [TestMethod]
  public void Execute()
  {
    TestClass test = new();

    test.Command.Execute();
    test.Command.Execute(null);
    Assert.AreEqual(2, test.CommandExecution);
  }

  [TestMethod]
  public void ExecuteWithParam()
  {
    TestClass test = new();

    test.IntCommand.Execute(1);
    test.IntCommand.Execute((object)2);
    Assert.AreEqual(2, test.IntegerCommandExecution);
  }
}
