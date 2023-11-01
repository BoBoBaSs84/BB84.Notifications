namespace BB84.NotificationsTests;

public sealed partial class BindablePropertyTests
{
  [TestMethod]
  public void Changed()
  {
    int changedValue = default;
    TestClass testClass = new(1);
    testClass.BindableProperty.Changed += (sender, e) => changedValue = e.Value;

    testClass.BindableProperty.Value = 2;

    Assert.AreEqual(2, changedValue);
    Assert.AreEqual(2, testClass.BindableProperty.Value);
  }
}
