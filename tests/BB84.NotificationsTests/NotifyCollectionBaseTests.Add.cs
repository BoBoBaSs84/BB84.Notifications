using System.ComponentModel;

using BB84.Notifications.Components;

namespace BB84.NotificationsTests;

public sealed partial class NotifyCollectionBaseTests
{
  [TestMethod]
  public void Adding()
  {
    CollectionChangeAction action = default!;
    MyCollection strings = new();
    strings.CollectionChanging += (s, e) => action = e.Action;

    strings.Add(UnitTestString);

    Assert.AreEqual(CollectionChangeAction.Add, action);
  }

  [TestMethod]
  public void Added()
  {
    CollectionChangeAction action = default!;
    string item = string.Empty;
    MyCollection strings = new();
    strings.CollectionChanged += (s, e) =>
    {
      if (e is CollectionChangedEventArgs<string> stringEvent)
        item = stringEvent.Item;
      action = e.Action;
    };

    strings.Add(UnitTestString);

    Assert.AreEqual(CollectionChangeAction.Add, action);
    Assert.AreEqual(UnitTestString, item);
  }
}
