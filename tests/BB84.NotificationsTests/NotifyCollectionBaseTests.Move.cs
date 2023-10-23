using BB84.Notifications.Enumerators;

namespace BB84.NotificationsTests;

public sealed partial class NotifyCollectionBaseTests
{
  [TestMethod]
  public void Moving()
  {
    CollectionChangeAction action = default!;
    int item = default;
    MyCollection strings = new() { "A", "B", "C" };
    strings.CollectionChanging += (sender, e) => { item = (int)e.Item!; action = e.Action; };

    strings.Move(0, 1);

    Assert.AreEqual(CollectionChangeAction.Move, action);
    Assert.AreEqual(0, item);
  }

  [TestMethod]
  public void Moved()
  {
    CollectionChangeAction action = default!;
    int item = default;
    MyCollection strings = new() { "A", "B", "C" };
    strings.CollectionChanged += (sender, e) => { item = (int)e.Item!; action = e.Action; };

    strings.Move(0, 1);

    Assert.AreEqual(CollectionChangeAction.Move, action);
    Assert.AreEqual(1, item);
  }
}
