// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Components;

namespace BB84.Notifications.Tests;

public sealed partial class NotifiablePropertyTests
{
  [TestMethod]
  public void Changed()
  {
    int changedValue = default;
    TestClass testClass = new(1);
    testClass.BindableProperty.PropertyChanged += (s, e) =>
    {
      if (e is PropertyChangedEventArgs<int> intEvent)
        changedValue = intEvent.Value;
    };

    testClass.BindableProperty.Value = 2;

    Assert.AreEqual(2, changedValue);
    Assert.AreEqual(2, testClass.BindableProperty.Value);
  }
}
