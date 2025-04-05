// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.NotificationsTests.Commands;

public sealed partial class AsyncActionCommandTests
{
  [TestMethod]
  public async Task Execute()
  {
    TestClass test = new();

    await test.Command.ExecuteAsync();
    test.Command.Execute(null);

    Assert.AreEqual(2, test.Execution);
  }

  [TestMethod]
  public async Task ExecuteWithParams()
  {
    TestClass test = new();

    await test.IntCommand.ExecuteAsync(1);
    test.IntCommand.Execute(2);

    Assert.AreEqual(2, test.IntegerExecution);
  }
}
