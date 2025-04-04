// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Extensions;

namespace BB84.NotificationsTests.Extensions;

[TestClass]
[SuppressMessage("Usage", "CA2201", Justification = "UnitTest")]
public sealed class TaskExtensionsTests
{
  [TestMethod]
  public void FireAndForgetSafeAsyncTest()
  {
    Task task = Task.Factory.StartNew(() => { });

    task.FireAndForgetSafeAsync();

    Assert.IsNotNull(task);
  }

  [TestMethod]
  public void FireAndForgetSafeAsyncExceptionHandledTest()
  {
    Task task = Task.Factory.StartNew(() => throw new Exception(""));

    task.FireAndForgetSafeAsync((e) => { });

    Assert.IsNotNull(task);
  }
}
