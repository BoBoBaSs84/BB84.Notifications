using System.Collections;
using System.Collections.ObjectModel;

using BB84.Notifications;
using BB84.Notifications.Enumerators;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class NotifyCollectionBaseTests
{
  private const string UnitTestString = "UnitTest";

  private sealed class MyCollection : NotifyCollectionBase, ICollection<string>
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
      RaiseCollectionChanging(CollectionChangeAction.Clear);
      _collection.Clear();
      RaiseCollectionChanged(CollectionChangeAction.Clear);
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

    public void Move(int oldIndex, int newIndex)
    {
      string @string = _collection[oldIndex];
      RaiseCollectionChanging(CollectionChangeAction.Move, oldIndex);
      _collection.RemoveAt(oldIndex);
      _collection.Insert(newIndex, @string);
      RaiseCollectionChanged(CollectionChangeAction.Move, newIndex);
    }

    IEnumerator IEnumerable.GetEnumerator()
      => _collection.GetEnumerator();

    public void Update(string oldString, string newString)
    {
      string @string = this.Where(x => x == oldString).Single();
      RaiseCollectionChanging(CollectionChangeAction.Replace, oldString);
      @string = newString;
      RaiseCollectionChanged(CollectionChangeAction.Replace, newString);
    }
  }
}
