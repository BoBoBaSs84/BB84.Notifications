using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Components;

/// <summary>
/// Provides data for the <see cref="INotifyCollectionChanged"/> event.
/// </summary>
/// <param name="action">The action that causes the change.</param>
public class CollectionChangedEventArgs(CollectionChangeAction action) : EventArgs
{
  /// <summary>
  /// The action that causes the change.
  /// </summary>
  public CollectionChangeAction Action { get; } = action;
}

/// <summary>
/// Provides data for the <see cref="INotifyCollectionChanged"/> event.
/// </summary>
/// <typeparam name="T">The type to work with.</typeparam>
/// <param name="action">The action that causes the change.</param>
/// <param name="item">The item that is changed.</param>
public sealed class CollectionChangedEventArgs<T>(CollectionChangeAction action, T item) : CollectionChangedEventArgs(action)
{
  /// <summary>
  /// The item that is changed.
  /// </summary>
  public T Item { get; } = item;
}

/// <summary>
/// Represents the method that will handle the event of the <see cref="INotifyCollectionChanged"/> interface.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">The argument that contains the event data.</param>
[SuppressMessage("Naming", "CA1711", Justification = "Event Handler Naming Conventions")]
public delegate void CollectionChangedEventHandler(object? sender, CollectionChangedEventArgs e);
