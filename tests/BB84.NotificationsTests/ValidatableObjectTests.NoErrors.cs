using System.Collections;

namespace BB84.NotificationsTests;

public sealed partial class ValidatableObjectTests
{
  [TestMethod]
  public void NoErrors()
  {
    TestClass tc = new()
    {
      Id = 1,
      FirstName = "John",
      LastName = "Doe",
      MiddleName = "Jimmy"
    };

    IEnumerable errors = tc.GetErrors(nameof(TestClass.Id));

    Assert.IsNotNull(errors);
    Assert.IsFalse(errors.GetEnumerator().MoveNext());
  }
}
