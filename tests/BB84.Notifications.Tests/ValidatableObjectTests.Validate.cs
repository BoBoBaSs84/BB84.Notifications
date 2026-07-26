// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

public sealed partial class ValidatableObjectTests
{
  [TestMethod]
  public void ValidateWithErrors()
  {
    TestClass tc = new(-23, "J", "D", "faksjdf;lkajsdf;lkasjdfl;kajsd;lfkjas;dfjkproituwerpu");

    bool isValid = tc.Validate();

    Assert.IsFalse(isValid);
    Assert.IsFalse(tc.IsValid);
    Assert.IsTrue(tc.HasErrors);
  }

  [TestMethod]
  public void ValidateWithNoErrors()
  {
    TestClass tc = new(1, "John", "Doe", "Jimmy");

    bool isValid = tc.Validate();

    Assert.IsTrue(isValid);
    Assert.IsTrue(tc.IsValid);
    Assert.IsFalse(tc.HasErrors);
  }

  [TestMethod]
  public void IsValidDoesNotTriggerValidation()
  {
    TestClass tc = new(-23, "J", "D");

    // Nothing has been validated yet, so no errors are recorded.
    Assert.IsTrue(tc.IsValid);
    Assert.IsFalse(tc.HasErrors);

    Assert.IsFalse(tc.Validate());
    Assert.IsFalse(tc.IsValid);
  }

  [TestMethod]
  public void ValidateClearsStaleErrors()
  {
    TestClass tc = new(-23, "John", "Doe");

    Assert.IsFalse(tc.Validate());
    Assert.IsTrue(tc.HasErrors);

    tc.SetIdWithoutValidation(1);

    List<string?> changedProperties = [];
    tc.ErrorsChanged += (s, e) => changedProperties.Add(e.PropertyName);

    Assert.IsTrue(tc.Validate());
    Assert.IsFalse(tc.HasErrors);
    Assert.IsFalse(tc.GetErrors(nameof(TestClass.Id)).GetEnumerator().MoveNext());
    CollectionAssert.Contains(changedProperties, nameof(TestClass.Id));
  }
}
