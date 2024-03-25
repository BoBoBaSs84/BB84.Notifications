using BB84.Notifications.Components;

namespace BB84.NotificationsTests;

public sealed partial class NotifiablePropertyTests
{
  [TestMethod]
  public void Changing()
  {
    int changingValue = default;
    TestClass testClass = new(1);
    testClass.BindableProperty.PropertyChanging += (s, e) =>
    {
      if (e is PropertyChangingEventArgs<int> intEvent)
        changingValue = intEvent.Value;
    };

    testClass.BindableProperty.Value = 2;

    Assert.AreEqual(1, changingValue);
    Assert.AreEqual(2, testClass.BindableProperty.Value);
  }
}
