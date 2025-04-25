using BB84.Notifications;

namespace BB84.NotificationsTests;

public sealed partial class ReversiblePropertyTests
{
  [TestMethod]
  public void ConvertValueTypeToPropertyTest()
  {
    int value = 42;

    ReversibleProperty<int> notifiableProperty = value;

    Assert.AreEqual(value, notifiableProperty.Value);
  }

  [TestMethod]
  public void ConvertPropertyToValueTypeTest()
  {
    ReversibleProperty<int> notifiableProperty = new(42);

    int value = notifiableProperty;

    Assert.AreEqual(notifiableProperty.Value, value);
  }
}
