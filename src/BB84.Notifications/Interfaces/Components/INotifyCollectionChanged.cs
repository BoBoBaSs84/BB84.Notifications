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
