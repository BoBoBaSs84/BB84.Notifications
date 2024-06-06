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
