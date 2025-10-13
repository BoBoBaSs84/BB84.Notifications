// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

public sealed partial class NotifiablePropertyTests
{
  [TestMethod]
  [DataRow(456, false)]
  [DataRow(6.3d, false)]
  [DataRow(0.75f, false)]
  [DataRow(45623415L, false)]
  [DataRow(0xFE, false)]
  [DataRow(true, false)]
  [DataRow("Test", false)]
  [DataRow(null, true)]
  public void IsNull(object? value, bool expected)
  {
    NotifiableProperty<object?> bindableProperty = new(value);

    Assert.AreEqual(expected, bindableProperty.IsNull);
  }
}
