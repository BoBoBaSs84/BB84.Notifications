using BB84.Notifications;
using BB84.Notifications.Attributes;

namespace BB84.NotificationsTests.Attributes;

[TestClass]
public class NotifyChangingAttributeTests
{
  [TestMethod]
  public void NotifyChangingAttributeTest()
  {
    List<string> propertiesNotified = new();
    TestClass testClass = new(3, 40);
    testClass.PropertyChanging += (sender, e) => propertiesNotified.Add(e.PropertyName!);

    testClass.Quantity = 4;

    Assert.AreEqual(2, propertiesNotified.Count);
    Assert.AreEqual(nameof(TestClass.Quantity), propertiesNotified.First());
    Assert.AreEqual(nameof(TestClass.TotalValue), propertiesNotified.Last());
  }

  private sealed class TestClass : NotifyPropertyBase
  {
    private int _quantity;
    private int _value;

    public TestClass(int quantity, int value)
    {
      Quantity = quantity;
      Value = value;
    }

    [NotifyChanging(nameof(TotalValue))]
    public int Quantity { get => _quantity; set => SetProperty(ref _quantity, value); }

    [NotifyChanging(nameof(TotalValue))]
    public int Value { get => _value; set => SetProperty(ref _value, value); }

    public int TotalValue => Quantity * Value;
  }
}
