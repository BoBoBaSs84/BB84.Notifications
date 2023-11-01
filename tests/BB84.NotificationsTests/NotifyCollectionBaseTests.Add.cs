using BB84.Notifications.Enumerators;

namespace BB84.NotificationsTests;

public sealed partial class NotifyCollectionBaseTests
{
  [TestMethod]
  public void Adding()
  {
    CollectionChangeAction action = default!;
    string item = string.Empty;
    MyCollection strings = new();
    strings.CollectionChanging += (sender, e) => { item = (string)e.Item!; action = e.Action; };

    strings.Add(UnitTestString);

    Assert.AreEqual(CollectionChangeAction.Add, action);
    Assert.AreEqual(UnitTestString, item);
  }

  [TestMethod]
  public void Added()
  {
    CollectionChangeAction action = default!;
    string item = string.Empty;
    MyCollection strings = new();
    strings.CollectionChanged += (sender, e) => { item = (string)e.Item!; action = e.Action; };

    strings.Add(UnitTestString);

    Assert.AreEqual(CollectionChangeAction.Add, action);
    Assert.AreEqual(UnitTestString, item);
  }
}
