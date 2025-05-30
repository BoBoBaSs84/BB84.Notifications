// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Components;

namespace BB84.Notifications.Interfaces.Components;

/// <summary>
/// Defines an interface for notifying clients that a collection is changing.
/// </summary>
/// <remarks>
/// Implement this interface to provide notifications when a collection is about to change.
/// This is typically used in scenarios where consumers need to react to changes in a collection
/// before they occur, such as canceling the change or preparing for it.
/// </remarks>
public interface INotifyCollectionChanging
{
  /// <summary>
  /// Occurs when the collection is about to change.
  /// </summary>
  /// <remarks>
  /// This event is raised before any modifications are made to the collection, allowing subscribers 
  /// to inspect or cancel the operation. The event handler can throw an exception to prevent the change.
  /// </remarks>
  event CollectionChangingEventHandler? CollectionChanging;
}
