using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

using BB84.Notifications;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class NotificationCollectionTests
{
  private const string UnitTestString = "UnitTest";

  private sealed class MyCollection : NotificationCollection, ICollection<string>
  {
    private readonly Collection<string> _collection;

    public MyCollection()
      => _collection = new Collection<string>();

    public int Count => _collection.Count;
    public bool IsReadOnly => false;

    public void Add(string item)
    {
      RaiseCollectionChanging(CollectionChangeAction.Add);
      _collection.Add(item);
      RaiseCollectionChanged(CollectionChangeAction.Add, item);
    }

    public void Clear()
    {
      RaiseCollectionChanging(CollectionChangeAction.Refresh);
      _collection.Clear();
      RaiseCollectionChanged(CollectionChangeAction.Refresh);
    }

    public bool Contains(string item)
      => _collection.Contains(item);

    public void CopyTo(string[] array, int arrayIndex)
      => _collection.CopyTo(array, arrayIndex);

    public IEnumerator<string> GetEnumerator()
      => _collection.GetEnumerator();

    public bool Remove(string item)
    {
      RaiseCollectionChanging(CollectionChangeAction.Remove, item);
      _ = _collection.Remove(item);
      RaiseCollectionChanged(CollectionChangeAction.Remove);
      return true;
    }

    IEnumerator IEnumerable.GetEnumerator()
      => _collection.GetEnumerator();
  }
}
