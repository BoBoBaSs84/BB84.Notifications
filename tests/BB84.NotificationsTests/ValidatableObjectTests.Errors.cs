// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.Collections;

namespace BB84.NotificationsTests;

public sealed partial class ValidatableObjectTests
{
  [TestMethod]
  public void Errors()
  {
    TestClass tc = new()
    {
      Id = -23,
      FirstName = "J",
      MiddleName = "faksjdf;lkajsdf;lkasjdfl;kajsd;lfkjas;dfjkproituwerpu",
      LastName = "D"
    };

    tc.Id = -12;
    tc.FirstName = string.Empty;

    IEnumerable idErrors = tc.GetErrors(nameof(TestClass.Id));
    IEnumerable fnErrors = tc.GetErrors(nameof(TestClass.FirstName));
    IEnumerable mnErrors = tc.GetErrors(nameof(TestClass.MiddleName));
    IEnumerable lnErrors = tc.GetErrors(nameof(TestClass.LastName));

    Assert.IsTrue(tc.HasErrors);
    Assert.IsTrue(idErrors.GetEnumerator().MoveNext());
    Assert.IsTrue(fnErrors.GetEnumerator().MoveNext());
    Assert.IsTrue(mnErrors.GetEnumerator().MoveNext());
    Assert.IsTrue(lnErrors.GetEnumerator().MoveNext());

  }
}
