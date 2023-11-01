using System.Collections;
using System.Collections.ObjectModel;

using BB84.Notifications;

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
      RaiseCollectionAdding(item);
      _collection.Add(item);
      RaiseCollectionAdded(item);
    }

    public void Clear()
    {
      RaiseCollectionClearing();
      _collection.Clear();
      RaiseCollectionCleared();
    }

    public bool Contains(string item)
      => _collection.Contains(item);

    public void CopyTo(string[] array, int arrayIndex)
      => _collection.CopyTo(array, arrayIndex);

    public IEnumerator<string> GetEnumerator()
      => _collection.GetEnumerator();

    public bool Remove(string item)
    {
      RaiseCollectionRemoving(item);
      _collection.Remove(item);
      RaiseCollectionRemoved(item);
      return true;
    }

    public void Move(int oldIndex, int newIndex)
    {
      string @string = _collection[oldIndex];
      RaiseCollectionMoving(oldIndex);
      _collection.RemoveAt(oldIndex);
      _collection.Insert(newIndex, @string);
      RaiseCollectionMoved(newIndex);
    }

    IEnumerator IEnumerable.GetEnumerator()
      => _collection.GetEnumerator();

    public void Update(string oldString, string newString)
    {
      var @string = this.Where(x => x == oldString).Single();
      RaiseCollectionReplacing(oldString);
      @string = newString;
      RaiseCollectionReplaced(newString);
    }
  }
}
