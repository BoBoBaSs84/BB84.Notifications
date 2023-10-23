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
  /// Raises the <see cref="CollectionChanged"/> event with the
  /// <see cref="CollectionChangeAction.Add"/> action.
  /// </summary>
  /// <param name="value">The object that is changed.</param>
  protected void RaiseCollectionAdded(object? value = null)
    => RaiseCollectionChanged(CollectionChangeAction.Add, value);

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event with the
  /// <see cref="CollectionChangeAction.Add"/> action.
  /// </summary>
  /// <param name="value">The object that is changing.</param>
  protected void RaiseCollectionAdding(object? value = null)
    => RaiseCollectionChanging(CollectionChangeAction.Add, value);

  /// <summary>
  /// Raises the <see cref="CollectionChanged"/> event with the
  /// <see cref="CollectionChangeAction.Clear"/> action.
  /// </summary>
  /// <param name="value">The object that is changed.</param>
  protected void RaiseCollectionCleared(object? value = null)
    => RaiseCollectionChanged(CollectionChangeAction.Clear, value);

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event with the
  /// <see cref="CollectionChangeAction.Clear"/> action.
  /// </summary>
  /// <param name="value">The object that is changing.</param>
  protected void RaiseCollectionClearing(object? value = null)
    => RaiseCollectionChanging(CollectionChangeAction.Clear, value);

  /// <summary>
  /// Raises the <see cref="CollectionChanged"/> event with the
  /// <see cref="CollectionChangeAction.Move"/> action.
  /// </summary>
  /// <param name="value">The object that is changed.</param>
  protected void RaiseCollectionMoved(object? value = null)
    => RaiseCollectionChanged(CollectionChangeAction.Move, value);

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event with the
  /// <see cref="CollectionChangeAction.Move"/> action.
  /// </summary>
  /// <param name="value">The object that is changing.</param>
  protected void RaiseCollectionMoving(object? value = null)
    => RaiseCollectionChanging(CollectionChangeAction.Move, value);

  /// <summary>
  /// Raises the <see cref="CollectionChanged"/> event with the
  /// <see cref="CollectionChangeAction.Remove"/> action.
  /// </summary>
  /// <param name="value">The object that is changed.</param>
  protected void RaiseCollectionRemoved(object? value = null)
    => RaiseCollectionChanged(CollectionChangeAction.Remove, value);

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event with the
  /// <see cref="CollectionChangeAction.Remove"/> action.
  /// </summary>
  /// <param name="value">The object that is changing.</param>
  protected void RaiseCollectionRemoving(object? value = null)
    => RaiseCollectionChanging(CollectionChangeAction.Remove, value);

  /// <summary>
  /// Raises the <see cref="CollectionChanged"/> event with the
  /// <see cref="CollectionChangeAction.Replace"/> action.
  /// </summary>
  /// <param name="value">The object that is changed.</param>
  protected void RaiseCollectionReplaced(object? value = null)
    => RaiseCollectionChanged(CollectionChangeAction.Replace, value);

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event with the
  /// <see cref="CollectionChangeAction.Replace"/> action.
  /// </summary>
  /// <param name="value">The object that is changing.</param>
  protected void RaiseCollectionReplacing(object? value = null)
    => RaiseCollectionChanging(CollectionChangeAction.Replace, value);

  /// <summary>
  /// Raises the <see cref="CollectionChanged"/> event.
  /// </summary>
  /// <param name="action">The action that causes the change.</param>
  /// <param name="value">The object that is changed.</param>
  private void RaiseCollectionChanged(CollectionChangeAction action, object? value = null)
    => CollectionChanged?.Invoke(this, new(action, value));

  /// <summary>
  /// Raises the <see cref="CollectionChanging"/> event.
  /// </summary>
  /// <param name="action">The action that causes the change.</param>
  /// <param name="value">The object that is changing.</param>
  private void RaiseCollectionChanging(CollectionChangeAction action, object? value = null)
    => CollectionChanging?.Invoke(this, new(action, value));
}
