using BB84.Notifications.Extensions;
using BB84.Notifications.Interfaces.Components;

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
    VoidHandler handler = new();
    Task task = Task.Factory.StartNew(() => throw new Exception(""));

    task.FireAndForgetSafeAsync(handler);
  }

  private sealed class VoidHandler : IExceptionHandler
  {
    public void HandleError(Exception exception) { }
  }
}
