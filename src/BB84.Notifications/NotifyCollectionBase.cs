using BB84.Notifications.Components;
using BB84.Notifications.Enumerators;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
/// The notify collection base class.
/// </summary>
public abstract class NotifyCollectionBase : NotifyPropertyBase, INotifyCollectionBase
{
  /// <inheritdoc/>
  public event CollectionChangedEventHandler? CollectionChanged;

  /// <inheritdoc/>
  public event CollectionChangingEventHandler? CollectionChanging;

  /// <summary>
  /// Raises the <see cref="CollectionChanged"/> event.
  /// </summary>
  /// <param name="action">The action that causes the change.</param>
  protected void RaiseCollectionChanged(CollectionChangeAction action)
    => CollectionChanged?.Invoke(this, new(action));

  /// <summary>
  /// Raises the <see cref="CollectionChanged"/> event.
  /// </summary>
  /// <typeparam name="T">The type to work with.</typeparam>
  /// <param name="action">The action that causes the change.</param>
  /// <param name="item">The item that is changed.</param>
  protected void RaiseCollectionChanged<T>(CollectionChangeAction action, T item)
    => CollectionChanged?.Invoke(this, new CollectionChangedEventArgs<T>(action, item));

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event.
  /// </summary>
  /// <param name="action">The action that causes the change.</param>
  protected void RaiseCollectionChanging(CollectionChangeAction action)
    => CollectionChanging?.Invoke(this, new(action));

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event.
  /// </summary>
  /// <typeparam name="T">The type to work with.</typeparam>
  /// <param name="action">The action that causes the change.</param>
  /// <param name="item">The item that is changing.</param>
  protected void RaiseCollectionChanging<T>(CollectionChangeAction action, T item)
    => CollectionChanging?.Invoke(this, new CollectionChangingEventArgs<T>(action, item));
}
