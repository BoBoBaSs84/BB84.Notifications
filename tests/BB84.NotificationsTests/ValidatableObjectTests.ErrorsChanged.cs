namespace BB84.NotificationsTests;

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
