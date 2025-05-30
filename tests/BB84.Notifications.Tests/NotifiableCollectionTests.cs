﻿// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BB84.Notifications.Tests;

[TestClass]
public sealed partial class NotifiableCollectionTests
{
  private const string UnitTestString = "UnitTest";

  private sealed class MyCollection : NotifiableCollection, ICollection<string>
  {
    private readonly Collection<string> _collection;

    public MyCollection()
      => _collection = [];

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
