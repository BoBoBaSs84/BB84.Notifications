using BB84.Notifications.Components;
using BB84.Notifications.Enumerators;

namespace BB84.NotificationsTests;

public sealed partial class NotifyCollectionBaseTests
{
  [TestMethod]
  public void Moving()
  {
    CollectionChangeAction action = default;
    int item = default;
    MyCollection strings = new() { "A", "B", "C" };
    strings.CollectionChanging += (sender, e) =>
    {
      if (e is CollectionChangingEventArgs<int> intEvent)
        item = intEvent.Item;
      action = e.Action;
    };

    strings.Move(1, 2);

    Assert.AreEqual(CollectionChangeAction.Move, action);
    Assert.AreEqual(1, item);
  }

  [TestMethod]
  public void Moved()
  {
    CollectionChangeAction action = default!;
    int item = default;
    MyCollection strings = new() { "A", "B", "C" };
    strings.CollectionChanged += (sender, e) =>
    {
      if (e is CollectionChangedEventArgs<int> intEvent)
        item = intEvent.Item;
      action = e.Action;
    };

    strings.Move(1, 2);

    Assert.AreEqual(CollectionChangeAction.Move, action);
    Assert.AreEqual(2, item);
  }
}
