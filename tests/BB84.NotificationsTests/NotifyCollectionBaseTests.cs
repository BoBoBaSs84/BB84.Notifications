using System.Collections;
using System.Collections.ObjectModel;

using BB84.Notifications;
using BB84.Notifications.Enumerators;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class NotifyCollectionBaseTests
{
  private sealed class MyCollection : NotifyCollectionBase, ICollection<string>
  {
    private readonly Collection<string> _collection;

    public MyCollection()
      => _collection = new Collection<string>();

    public int Count => _collection.Count;
    public bool IsReadOnly => false;

    public void Add(string item)
    {
      RaiseCollectionChanging(CollectionChangeAction.Add, item);
      _collection.Add(item);
      RaiseCollectionChanged(CollectionChangeAction.Add, item);
    }

    public void Clear()
      => _collection.Clear();

    public bool Contains(string item)
      => _collection.Contains(item);

    public void CopyTo(string[] array, int arrayIndex)
      => _collection.CopyTo(array, arrayIndex);

    public IEnumerator<string> GetEnumerator()
      => _collection.GetEnumerator();

    public bool Remove(string item)
      => _collection.Remove(item);

    IEnumerator IEnumerable.GetEnumerator()
      => _collection.GetEnumerator();
  }
}
