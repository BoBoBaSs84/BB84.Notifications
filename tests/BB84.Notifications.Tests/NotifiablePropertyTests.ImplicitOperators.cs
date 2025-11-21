// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

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
