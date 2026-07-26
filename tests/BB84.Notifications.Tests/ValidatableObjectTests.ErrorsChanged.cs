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
    List<string?> changedProperties = [];
    TestClass tc = new();
    tc.ErrorsChanged += (s, e) => changedProperties.Add(e.PropertyName);

    tc.Id = -12;
    tc.Id = 12;

    Assert.IsFalse(tc.HasErrors);
    Assert.IsTrue(changedProperties.Count > 0);
  }

  [TestMethod]
  public void ErrorsChangedReportsTheAffectedProperty()
  {
    List<string?> changedProperties = [];
    TestClass tc = new();
    tc.ErrorsChanged += (s, e) => changedProperties.Add(e.PropertyName);

    // Recording the error notifies for Id, and clearing it again must notify
    // for Id as well - not for the name of the member that did the clearing.
    tc.Id = -12;
    tc.Id = 12;

    CollectionAssert.AreEqual(
      new string?[] { nameof(TestClass.Id), nameof(TestClass.Id) },
      changedProperties);
  }
}
