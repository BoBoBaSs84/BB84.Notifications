// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Components;

namespace BB84.NotificationsTests;

public sealed partial class NotifiableObjectTests
{
  [TestMethod]
  public void Changing()
  {
    string? propertyName = string.Empty;
    TestClass testClass = new() { Property = 1 };
    testClass.PropertyChanging += (sender, e) => propertyName = e.PropertyName;

    testClass.Property = 2;

    Assert.AreEqual(nameof(testClass.Property), propertyName);
    Assert.AreNotEqual(1, testClass.Property);
  }

  [TestMethod]
  public void ChangingWithValue()
  {
    string? propertyName = string.Empty;
    int propertyValue = default;
    TestClass testClass = new() { Property = 1 };
    testClass.PropertyChanging += (sender, e) =>
    {
      if (e is PropertyChangingEventArgs<int> intEvent)
        propertyValue = intEvent.Value;

      propertyName = e.PropertyName;
    };

    testClass.Property = 2;

    Assert.AreEqual(nameof(testClass.Property), propertyName);
    Assert.AreEqual(1, propertyValue);
    Assert.AreEqual(2, testClass.Property);
  }
}
