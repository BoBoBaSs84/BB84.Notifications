// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

public sealed partial class ReversiblePropertyTests
{
  [TestMethod]
  [TestCategory("Method")]
  public void SetPropertyShouldAddAndSetNewValue()
  {
    int newValue = 1;
    TestClass testClass = new();

    testClass.Property.Value = newValue;

    Assert.IsFalse(testClass.Property.HasNextValue);
    Assert.IsTrue(testClass.Property.HasPreviousValue);
    Assert.IsFalse(testClass.Property.IsDefault);
    Assert.IsFalse(testClass.Property.IsNull);
    Assert.AreEqual(2, testClass.Property.Count);
    Assert.AreEqual(1, testClass.Property.Index);
    Assert.AreEqual(newValue, testClass.Property.Value);
  }

  [TestMethod]
  [TestCategory("Method")]
  public void SetPropertyAboveTenShouldAddAndSetNewValue()
  {
    int startValue = 1, endValue = 10;
    TestClass testClass = new();

    for (int i = startValue; i <= endValue; i++)
    {
      testClass.Property.Value = i;
    }

    Assert.IsFalse(testClass.Property.HasNextValue);
    Assert.IsTrue(testClass.Property.HasPreviousValue);
    Assert.IsFalse(testClass.Property.IsDefault);
    Assert.IsFalse(testClass.Property.IsNull);
    Assert.AreEqual(10, testClass.Property.Count);
    Assert.AreEqual(9, testClass.Property.Index);
    Assert.AreEqual(endValue, testClass.Property.Value);
  }

  [TestMethod]
  [TestCategory("Method")]
  public void SetPropertyShouldThrowNotifications()
  {
    bool wasChanging = default, hasChanged = default;
    int newValue = 1;
    TestClass testClass = new();
    testClass.Property.PropertyChanging += (s, e) => wasChanging = true;
    testClass.Property.PropertyChanged += (s, e) => hasChanged = true;

    testClass.Property.Value = newValue;

    Assert.IsFalse(testClass.Property.HasNextValue);
    Assert.IsTrue(testClass.Property.HasPreviousValue);
    Assert.IsFalse(testClass.Property.IsDefault);
    Assert.IsFalse(testClass.Property.IsNull);
    Assert.IsTrue(wasChanging);
    Assert.IsTrue(hasChanged);
    Assert.AreEqual(2, testClass.Property.Count);
    Assert.AreEqual(1, testClass.Property.Index);
    Assert.AreEqual(newValue, testClass.Property.Value);
  }
}
