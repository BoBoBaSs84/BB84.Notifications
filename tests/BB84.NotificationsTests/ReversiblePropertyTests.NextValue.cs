// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.NotificationsTests;

public sealed partial class ReversiblePropertyTests
{
  [TestMethod]
  [TestCategory("Method")]
  public void NextValueShouldDoNothingWhenThereIsNoNextValue()
  {
    TestClass testClass = new();

    testClass.Property.NextValue();

    Assert.AreEqual(default, testClass.Property.Value);
  }

  [TestMethod]
  [TestCategory("Method")]
  public void NextValueShouldChangeValueWhenThereIsNextValue()
  {
    int newValue = 1;
    TestClass testClass = new();
    testClass.Property.Value = newValue;
    testClass.Property.PreviousValue();

    testClass.Property.NextValue();

    Assert.IsFalse(testClass.Property.HasNextValue);
    Assert.IsTrue(testClass.Property.HasPreviousValue);
    Assert.IsFalse(testClass.Property.IsDefault);
    Assert.IsFalse(testClass.Property.IsNull);
    Assert.AreEqual(newValue, testClass.Property.Value);
  }
}
