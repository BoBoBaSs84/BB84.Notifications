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
