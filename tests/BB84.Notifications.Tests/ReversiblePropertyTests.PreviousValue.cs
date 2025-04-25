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
  public void PreviousValueShouldDoNothingWhenThereIsNoPreviousValue()
  {
    TestClass testClass = new();

    testClass.Property.PreviousValue();

    Assert.IsFalse(testClass.Property.HasNextValue);
    Assert.IsFalse(testClass.Property.HasPreviousValue);
    Assert.IsTrue(testClass.Property.IsDefault);
    Assert.IsFalse(testClass.Property.IsNull);
    Assert.AreEqual(default, testClass.Property.Value);
  }

  [TestMethod]
  [TestCategory("Method")]
  public void PreviousValueShouldChangeValueWhenThereIsPreviousValue()
  {
    int newValue = 1;
    TestClass testClass = new();
    testClass.Property.Value = newValue;

    testClass.Property.PreviousValue();

    Assert.IsTrue(testClass.Property.HasNextValue);
    Assert.IsFalse(testClass.Property.HasPreviousValue);
    Assert.IsTrue(testClass.Property.IsDefault);
    Assert.IsFalse(testClass.Property.IsNull);
    Assert.AreNotEqual(newValue, testClass.Property.Value);
  }
}
