using BB84.Notifications.Enumerators;

namespace BB84.NotificationsTests;

public sealed partial class NotifyCollectionBaseTests
{
  [TestMethod]
  public void Replacing()
  {
    CollectionChangeAction action = default!;
    string item = string.Empty;
    MyCollection strings = new() { UnitTestString };
    strings.CollectionChanging += (sender, e) => { item = (string)e.Item!; action = e.Action; };

    strings.Update(UnitTestString, "UnitTest");

    Assert.AreEqual(CollectionChangeAction.Replace, action);
    Assert.AreEqual(UnitTestString, item);
  }

  [TestMethod]
  public void Replaced()
  {
    CollectionChangeAction action = default!;
    string item = string.Empty;
    MyCollection strings = new() { UnitTestString };
    strings.CollectionChanged += (sender, e) => { item = (string)e.Item!; action = e.Action; };

    strings.Update(UnitTestString, "UnitTest");

    Assert.AreEqual(CollectionChangeAction.Replace, action);
    Assert.AreEqual("UnitTest", item);
  }
}
