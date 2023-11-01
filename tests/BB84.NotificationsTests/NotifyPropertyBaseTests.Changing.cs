namespace BB84.NotificationsTests;

public sealed partial class NotifyPropertyBaseTests
{
  [TestMethod]
  public void Changing()
  {
    string changingProperty = string.Empty;
    int changingValue = default;
    TestClass testClass = new() { Property = 1 };
    testClass.PropertyChanging += (sender, e) => { changingValue = (int)e.Value!; changingProperty = e.Name; };

    testClass.Property = 2;

    Assert.AreEqual(nameof(testClass.Property), changingProperty);
    Assert.AreEqual(1, changingValue);
    Assert.AreNotEqual(1, testClass.Property);
  }
}
