using BB84.Notifications.Components;
using BB84.Notifications.Enumerators;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
/// The notify collection base class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="NotifyPropertyBase"/> class and implements
/// the members of the <see cref="INotifyCollectionBase"/> interface.
/// </remarks>
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
  /// <param name="value">The item that is changed.</param>
  protected void RaiseCollectionChanged(CollectionChangeAction action, object? value = null)
    => CollectionChanged?.Invoke(this, new(action, value));

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event.
  /// </summary>
  /// <param name="action">The action that causes the change.</param>
  /// <param name="value">The item that is changing.</param>
  protected void RaiseCollectionChanging(CollectionChangeAction action, object? value = null)
    => CollectionChanging?.Invoke(this, new(action, value));
}
