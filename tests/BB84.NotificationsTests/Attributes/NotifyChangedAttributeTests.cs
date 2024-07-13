using BB84.Notifications;
using BB84.Notifications.Attributes;

namespace BB84.NotificationsTests.Attributes;

[TestClass]
public class NotifyChangedAttributeTests
{
  [TestMethod]
  public void NotifyChangedAttributeTest()
  {
    List<string> propertiesNotified = [];
    TestClass testClass = new(3, 40);
    testClass.PropertyChanged += (sender, e) => propertiesNotified.Add(e.PropertyName!);

    testClass.Value = 50;

    Assert.AreEqual(2, propertiesNotified.Count);
    Assert.AreEqual(nameof(TestClass.Value), propertiesNotified.First());
    Assert.AreEqual(nameof(TestClass.TotalValue), propertiesNotified.Last());
  }

  private sealed class TestClass : NotifiableObject
  {
    private int _quantity;
    private int _value;

    public TestClass(int quantity, int value)
    {
      Quantity = quantity;
      Value = value;
    }

    [NotifyChanged(nameof(TotalValue))]
    public int Quantity
    {
      get => _quantity;
      set => SetProperty(ref _quantity, value);
    }

    [NotifyChanged(nameof(TotalValue))]
    public int Value
    {
      get => _value;
      set => SetProperty(ref _value, value);
    }

    public int TotalValue => Quantity * Value;
  }
}
