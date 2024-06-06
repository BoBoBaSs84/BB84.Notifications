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
    bool invoked = false;
    Task task = Task.Factory.StartNew(() => throw new Exception(""));

    task.FireAndForgetSafeAsync((e) => invoked = true);
    Task.Delay(100).Wait();

    Assert.IsTrue(invoked);
  }
}
