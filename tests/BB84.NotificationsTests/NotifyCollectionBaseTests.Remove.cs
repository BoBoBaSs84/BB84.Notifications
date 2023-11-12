using BB84.Notifications.Components;
using BB84.Notifications.Enumerators;

namespace BB84.NotificationsTests;

public sealed partial class NotifyCollectionBaseTests
{
  [TestMethod]
  public void Removing()
  {
    CollectionChangeAction action = default!;
    string item = string.Empty;
    MyCollection strings = new() { UnitTestString };
    strings.CollectionChanging += (sender, e) =>
    {
      if (e is CollectionChangingEventArgs<string> stringEvent)
        item = stringEvent.Item;
      action = e.Action;
    };

    _ = strings.Remove(UnitTestString);

    Assert.AreEqual(CollectionChangeAction.Remove, action);
    Assert.AreEqual(UnitTestString, item);
  }

  [TestMethod]
  public void Removed()
  {
    CollectionChangeAction action = default!;
    string item = string.Empty;
    MyCollection strings = new() { UnitTestString };
    strings.CollectionChanged += (sender, e) => action = e.Action;

    _ = strings.Remove(UnitTestString);

    Assert.AreEqual(CollectionChangeAction.Remove, action);
  }
}
