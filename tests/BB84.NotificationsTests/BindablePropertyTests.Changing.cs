namespace BB84.NotificationsTests;

public sealed partial class BindablePropertyTests
{
  [TestMethod]
  public void Changing()
  {
    int changingValue = default;
    TestClass testClass = new(1);
    testClass.BindableProperty.Changing += (sender, e) => changingValue = e.Value;

    testClass.BindableProperty.Value = 2;

    Assert.AreEqual(1, changingValue);
    Assert.AreEqual(2, testClass.BindableProperty.Value);
  }
}
