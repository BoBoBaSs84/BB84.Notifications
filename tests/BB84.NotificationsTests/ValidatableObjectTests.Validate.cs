// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.NotificationsTests;

public sealed partial class ValidatableObjectTests
{
  [TestMethod]
  public void ValidateWithErrors()
  {
    TestClass tc = new(-23, "J", "D", "faksjdf;lkajsdf;lkasjdfl;kajsd;lfkjas;dfjkproituwerpu");

    bool isValid = tc.IsValid;

    Assert.IsFalse(isValid);
  }

  [TestMethod]
  public void ValidateWithNoErrors()
  {
    TestClass tc = new(1, "John", "Doe", "Jimmy");

    bool isValid = tc.IsValid;

    Assert.IsTrue(isValid);
  }
}
