using BB84.Notifications.Enumerators;

namespace BB84.NotificationsTests;

public sealed partial class NotifyCollectionBaseTests
{
  [TestMethod]
  public void Clearing()
  {
    CollectionChangeAction action = default!;
    MyCollection strings = new() { UnitTestString };
    strings.CollectionChanging += (sender, e) => action = e.Action;

    strings.Clear();

    Assert.AreEqual(CollectionChangeAction.Clear, action);
  }

  [TestMethod]
  public void Cleared()
  {
    CollectionChangeAction action = default!;
    MyCollection strings = new() { UnitTestString };
    strings.CollectionChanged += (sender, e) => action = e.Action;

    strings.Clear();

    Assert.AreEqual(CollectionChangeAction.Clear, action);
  }
}
