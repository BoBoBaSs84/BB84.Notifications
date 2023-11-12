using BB84.Notifications.Components;
using BB84.Notifications.Enumerators;

namespace BB84.NotificationsTests;

public sealed partial class NotifyCollectionBaseTests
{
  [TestMethod]
  public void Replacing()
  {
    CollectionChangeAction action = default;
    string item = string.Empty;
    MyCollection strings = new() { UnitTestString };
    strings.CollectionChanging += (sender, e) =>
    {
      if (e is CollectionChangingEventArgs<string> stringEvent)
        item = stringEvent.Item;
      action = e.Action;
    };

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
    strings.CollectionChanged += (sender, e) =>
    {
      if (e is CollectionChangedEventArgs<string> stringEvent)
        item = stringEvent.Item;
      action = e.Action;
    };

    strings.Update(UnitTestString, "UnitTest");

    Assert.AreEqual(CollectionChangeAction.Replace, action);
    Assert.AreEqual("UnitTest", item);
  }
}
