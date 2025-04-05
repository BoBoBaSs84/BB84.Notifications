// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications;
using BB84.Notifications.Interfaces;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class NotifiablePropertyTests
{
  private sealed class TestClass(int bindableProperty) : ITestClass
  {
    public INotifiableProperty<int> BindableProperty { get; set; } = new NotifiableProperty<int>(bindableProperty);
  }

  private interface ITestClass
  {
    INotifiableProperty<int> BindableProperty { get; set; }
  }
}
