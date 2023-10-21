using BB84.Notifications.Enumerators;

namespace BB84.NotificationsTests;

public sealed partial class NotifyCollectionBaseTests
{
  [TestMethod]
  public void Changed()
  {
    CollectionChangeAction action = default!;
    string item = string.Empty;
    MyCollection strings = new();
    strings.CollectionChanged += (sender, e) => { item = (string)e.Item!; action = e.Action; };

    strings.Add("UnitTest");

    Assert.AreEqual(CollectionChangeAction.Add, action);
    Assert.AreEqual("UnitTest", item);
  }
}
