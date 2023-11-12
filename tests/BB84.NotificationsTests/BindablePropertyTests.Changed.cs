using BB84.Notifications.Components;

namespace BB84.NotificationsTests;

public sealed partial class BindablePropertyTests
{
  [TestMethod]
  public void Changed()
  {
    int changedValue = default;
    TestClass testClass = new(1);
    testClass.BindableProperty.PropertyChanged += (s, e) =>
    {
      if (e is PropertyChangedEventArgs<int> intEvent)
        changedValue = intEvent.Value;
    };

    testClass.BindableProperty.Value = 2;

    Assert.AreEqual(2, changedValue);
    Assert.AreEqual(2, testClass.BindableProperty.Value);
  }
}
