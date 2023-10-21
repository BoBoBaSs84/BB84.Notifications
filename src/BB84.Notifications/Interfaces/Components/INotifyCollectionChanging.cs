using BB84.Notifications.Components;

namespace BB84.Notifications.Interfaces.Components;

/// <summary>
/// The notify collection changing interface.
/// </summary>
public interface INotifyCollectionChanging
{
  /// <summary>
  /// Occurs when the collection is changing.
  /// </summary>
  event CollectionChangingEventHandler? CollectionChanging;
}
