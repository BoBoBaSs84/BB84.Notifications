﻿// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Components;

namespace BB84.Notifications.Tests;

public sealed partial class NotifiableObjectTests
{
  [TestMethod]
  public void Changed()
  {
    string? propertyName = string.Empty;
    TestClass testClass = new();
    testClass.PropertyChanged += (sender, e) => propertyName = e.PropertyName;

    testClass.Property = 1;

    Assert.AreEqual(nameof(testClass.Property), propertyName);
    Assert.AreEqual(1, testClass.Property);
  }

  [TestMethod]
  public void ChangedWithValue()
  {
    string? propertyName = string.Empty;
    int propertyValue = default;
    TestClass testClass = new();
    testClass.PropertyChanged += (sender, e) =>
    {
      if (e is PropertyChangedEventArgs<int> iArgs)
        propertyValue = iArgs.Value;

      propertyName = e.PropertyName;
    };

    testClass.Property = 1;

    Assert.AreEqual(nameof(testClass.Property), propertyName);
    Assert.AreEqual(1, propertyValue);
    Assert.AreEqual(1, testClass.Property);
  }
}
