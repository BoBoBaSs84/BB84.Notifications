using BB84.Notifications;

namespace BB84.NotificationsTests;

public sealed partial class NotifiablePropertyTests
{
  [TestMethod]
  public void ConvertValueTypeToPropertyTest()
  {
    int value = 42;

    NotifiableProperty<int> notifiableProperty = value;

    Assert.AreEqual(value, notifiableProperty.Value);
  }

  [TestMethod]
  public void ConvertPropertyToValueTypeTest()
  {
    NotifiableProperty<int> notifiableProperty = new(42);

    int value = notifiableProperty;

    Assert.AreEqual(notifiableProperty.Value, value);
  }
}
