using BB84.Notifications.Components;

namespace BB84.Notifications.Interfaces.Components;

/// <summary>
/// The notify collection changed interface.
/// </summary>
public interface INotifyCollectionChanged
{
  /// <summary>
  /// Occurs when the collection is changed.
  /// </summary>
  event CollectionChangedEventHandler? CollectionChanged;
}
