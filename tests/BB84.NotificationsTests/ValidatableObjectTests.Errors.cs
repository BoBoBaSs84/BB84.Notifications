using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
