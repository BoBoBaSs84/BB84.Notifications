namespace BB84.NotificationsTests;

public sealed partial class NotifyPropertyBaseTests
{
  [TestMethod]
  public void Changed()
  {
    string changedProperty = string.Empty;
    int changedValue = default;
    TestClass testClass = new();
    testClass.PropertyChanged += (sender, e) => { changedValue = (int)e.Value!; changedProperty = e.Name; };

    testClass.Property = 1;

    Assert.AreEqual(nameof(testClass.Property), changedProperty);
    Assert.AreEqual(1, changedValue);
    Assert.AreEqual(1, testClass.Property);
  }
}
