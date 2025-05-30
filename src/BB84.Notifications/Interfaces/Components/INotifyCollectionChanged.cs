// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Components;

namespace BB84.Notifications.Interfaces.Components;

/// <summary>
/// Provides notification when the contents of a collection change.
/// </summary>
/// <remarks>
/// This interface is typically implemented by collections to notify subscribers of changes
/// such as additions, removals, or modifications to the collection. It is commonly used in
/// data-binding scenarios to update UI elements when the underlying collection changes.
/// </remarks>
public interface INotifyCollectionChanged
{
  /// <summary>
  /// Occurs when the collection is changed, such as when items are added, removed,
  /// or the entire collection is refreshed.
  /// </summary>
  /// <remarks>
  /// This event is typically used to notify subscribers of changes to the collection's contents.
  /// Handlers for this event can inspect the event arguments to determine the nature of the change.
  /// </remarks>
  event CollectionChangedEventHandler? CollectionChanged;
}
