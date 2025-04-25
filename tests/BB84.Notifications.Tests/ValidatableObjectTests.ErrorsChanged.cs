// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

public sealed partial class ValidatableObjectTests
{
  [TestMethod]
  public void ErrorsChanged()
  {
    bool changed = false;
    TestClass tc = new();
    tc.ErrorsChanged += (s, e) => changed = true;

    tc.Id = -12;
    tc.Id = 12;

    Assert.IsFalse(tc.HasErrors);
    Assert.IsTrue(changed);
  }
}
