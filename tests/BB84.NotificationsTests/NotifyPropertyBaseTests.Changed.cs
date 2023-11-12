using BB84.Notifications.Components;

namespace BB84.NotificationsTests;

public sealed partial class NotifyPropertyBaseTests
{
  [TestMethod]
  public void Changed()
  {
    string changedProperty = string.Empty;
    TestClass testClass = new();
    testClass.PropertyChanged += (sender, e) => changedProperty = e.Name;

    testClass.Property = 1;

    Assert.AreEqual(nameof(testClass.Property), changedProperty);
    Assert.AreEqual(1, testClass.Property);
  }

  [TestMethod]
  public void ChangedWithValue()
  {
    string propertyName = string.Empty;
    int propertyValue = default;
    TestClass testClass = new();
    testClass.PropertyChanged += (sender, e) =>
    {
      if (e is PropertyChangedEventArgs<int> iArgs)
        propertyValue = iArgs.Value;

      propertyName = e.Name;
    };

    testClass.Property = 1;

    Assert.AreEqual(nameof(testClass.Property), propertyName);
    Assert.AreEqual(1, propertyValue);
    Assert.AreEqual(1, testClass.Property);
  }
}
