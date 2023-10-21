﻿using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Enumerators;
using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Components;

/// <summary>
/// The collection changed event args class.
/// </summary>
public sealed class CollectionChangedEventArgs : EventArgs
{
  /// <summary>
  /// Initializes a instance of the collection changed event args class.
  /// </summary>
  /// <param name="type">The event type that causes the change.</param>
  /// <param name="item">The item that is changed.</param>
  public CollectionChangedEventArgs(CollectionEventType type, object? item)
  {
    Event = type;
    Item = item;
  }

  /// <summary>
  /// The event type that causes the change.
  /// </summary>
  public CollectionEventType Event { get; }

  /// <summary>
  /// The item that is changed.
  /// </summary>
  public object? Item { get; }
}

/// <summary>
/// Represents the method that will handle the event of an <see cref="INotifyCollectionChanged"/> interface.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">The argument that contains the event data.</param>
[SuppressMessage("Naming", "CA1711", Justification = "Event Handler Naming Conventions")]
public delegate void CollectionChangedEventHandler(object? sender, CollectionChangedEventArgs e);
