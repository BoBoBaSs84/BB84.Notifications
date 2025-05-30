// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Components;

/// <summary>
/// Provides data for the collection changing event, including the action being performed.
/// </summary>
/// <remarks>
/// This class is typically used to describe upcoming changes to a collection, such as adding
/// or removing items. It includes the specific action performed on the collection.
/// </remarks>
/// <param name="action">The action that causes the change.</param>
public class CollectionChangingEventArgs(CollectionChangeAction action) : EventArgs
{
  /// <summary>
  /// Gets the type of action that caused the change to the collection.
  /// </summary>
  public CollectionChangeAction Action => action;
}

/// <summary>
/// Provides data for the collection changing event, including the action being performed
/// and the item involved.
/// </summary>
/// <remarks>
/// This class is typically used to describe upcoming changes to a collection, such as adding
/// or removing items. It includes the specific action performed on the collection and the
/// item involved in the change.
/// </remarks>
/// <typeparam name="T">The type of the item in the collection.</typeparam>
/// <param name="action">The action that causes the change.</param>
/// <param name="item">The item that is changing in the collection.</param>
public sealed class CollectionChangingEventArgs<T>(CollectionChangeAction action, T item) : CollectionChangingEventArgs(action)
{
  /// <summary>
  /// Gets the item stored in this instance.
  /// </summary>
  public T Item => item;
}

/// <summary>
/// Represents the method that will handle a collection changing event.
/// </summary>
/// <param name="sender">
/// The source of the event. This is typically the object whose collection is changing.
/// </param>
/// <param name="args">
/// An instance of <see cref="CollectionChangingEventArgs"/> that contains the event data.
/// </param>
[SuppressMessage("Naming", "CA1711", Justification = "Event Handler Naming Conventions")]
public delegate void CollectionChangingEventHandler(object? sender, CollectionChangingEventArgs args);
