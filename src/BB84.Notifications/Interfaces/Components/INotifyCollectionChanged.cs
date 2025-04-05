// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Components;

namespace BB84.Notifications.Interfaces.Components;

/// <summary>
/// Notifies clients that a collection has changed.
/// </summary>
public interface INotifyCollectionChanged
{
  /// <summary>
  /// Occurs when a collection changes.
  /// </summary>
  event CollectionChangedEventHandler? CollectionChanged;
}
