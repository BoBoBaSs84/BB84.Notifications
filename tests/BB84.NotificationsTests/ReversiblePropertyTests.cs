using BB84.Notifications;
using BB84.Notifications.Interfaces;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class ReversiblePropertyTests
{
  [TestMethod]
  [TestCategory("Constructor")]
  public void ReversiblePropertyTest()
  {
    TestClass? testClass;

    testClass = new();

    Assert.IsNotNull(testClass);
    Assert.IsFalse(testClass.Property.HasNextValue);
    Assert.IsFalse(testClass.Property.HasPreviousValue);
    Assert.IsTrue(testClass.Property.IsDefault);
    Assert.IsFalse(testClass.Property.IsNull);
    Assert.AreEqual(1, testClass.Property.Count);
    Assert.AreEqual(default, testClass.Property.Index);
    Assert.AreEqual(default, testClass.Property.Value);
  }

  private sealed class TestClass(int propertyValue) : ITestClass
  {
    public TestClass() : this(default)
    { }

    public IReversibleProperty<int> Property { get; set; } = new ReversibleProperty<int>(propertyValue);
  }

  private interface ITestClass
  {
    IReversibleProperty<int> Property { get; set; }
  }
}
