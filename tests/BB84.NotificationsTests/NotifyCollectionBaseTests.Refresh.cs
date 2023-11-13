using System.ComponentModel;

namespace BB84.NotificationsTests;

public sealed partial class NotifyCollectionBaseTests
{
  [TestMethod]
  public void Refreshing()
  {
    CollectionChangeAction action = default!;
    MyCollection strings = new() { UnitTestString };
    strings.CollectionChanging += (sender, e) => action = e.Action;

    strings.Clear();

    Assert.AreEqual(CollectionChangeAction.Refresh, action);
  }

  [TestMethod]
  public void Refreshed()
  {
    CollectionChangeAction action = default!;
    MyCollection strings = new() { UnitTestString };
    strings.CollectionChanged += (sender, e) => action = e.Action;

    strings.Clear();

    Assert.AreEqual(CollectionChangeAction.Refresh, action);
  }
}
