// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Attributes;

namespace BB84.Notifications.Tests.Attributes;

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

    Assert.HasCount(2, propertiesNotified);
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
