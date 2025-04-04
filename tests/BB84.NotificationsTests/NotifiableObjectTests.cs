// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class NotifiableObjectTests
{
  private sealed class TestClass : NotifiableObject
  {
    private int _property;

    public int Property
    {
      get => _property;
      set => SetProperty(ref _property, value);
    }
  }
}
