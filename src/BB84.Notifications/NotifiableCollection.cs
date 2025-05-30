// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Components;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
/// Represents a collection that provides notifications when its contents or properties change.
/// </summary>
[SuppressMessage("Naming", "CA1711", Justification = "Identifier is correct here")]
public abstract class NotifiableCollection : NotifiableObject, INotifiableCollection
{
  /// <inheritdoc/>
  public event CollectionChangedEventHandler? CollectionChanged;

  /// <inheritdoc/>
  public event CollectionChangingEventHandler? CollectionChanging;

  /// <summary>
  /// Raises the <see cref="CollectionChanged"/> event to notify subscribers of changes to the collection.
  /// </summary>
  /// <remarks>This method invokes the <see cref="CollectionChanged"/> event with the specified
  /// <paramref name="action"/>. Ensure that any subscribers to the event handle the change appropriately.
  /// </remarks>
  /// <param name="action">
  /// The type of change that occurred in the collection.
  /// This value indicates whether items were added, removed, or the collection was refreshed.
  /// </param>
  protected void RaiseCollectionChanged(CollectionChangeAction action)
    => CollectionChanged?.Invoke(this, new(action));

  /// <summary>
  /// Raises the <see cref="CollectionChanged"/> event to notify subscribers of changes to the collection. 
  /// </summary>
  /// <remarks>
  /// This method invokes the <see cref="CollectionChanged"/> event with the specified action and item.
  /// Ensure that any subscribers to the event handle the change appropriately.
  /// </remarks>
  /// <typeparam name="T">The type of the item affected by the collection change.</typeparam>
  /// <param name="action">
  /// The action that describes the type of change to the collection, such as Add or Remove.
  /// </param>
  /// <param name="item">
  /// The item that is affected by the collection change.
  /// </param>
  protected void RaiseCollectionChanged<T>(CollectionChangeAction action, T item)
    => CollectionChanged?.Invoke(this, new CollectionChangedEventArgs<T>(action, item));

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event to notify subscribers that the collection
  /// is about to change.
  /// </summary>
  /// <param name="action">
  /// The type of change that is occurring in the collection.
  /// This value indicates whether items are being added, removed or the entire collection is being refreshed.
  /// </param>
  protected void RaiseCollectionChanging(CollectionChangeAction action)
    => CollectionChanging?.Invoke(this, new(action));

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event to notify subscribers that the collection
  /// is about to change.
  /// </summary>
  /// <remarks>
  /// This method should be called before a change is made to the collection to allow subscribers
  /// to respond to the impending change.
  /// </remarks>
  /// <typeparam name="T">The type of the item involved in the collection change.</typeparam>
  /// <param name="action">
  /// The action indicating the type of change being performed on the collection.
  /// </param>
  /// <param name="item">
  /// The item that is affected by the upcoming collection change.
  /// </param>
  protected void RaiseCollectionChanging<T>(CollectionChangeAction action, T item)
    => CollectionChanging?.Invoke(this, new CollectionChangingEventArgs<T>(action, item));
}
