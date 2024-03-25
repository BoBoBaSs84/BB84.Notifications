using BB84.Notifications.Components;

namespace BB84.Notifications.Interfaces.Components;

/// <summary>
/// Notifies clients that a collection is changing.
/// </summary>
public interface INotifyCollectionChanging
{
  /// <summary>
  /// Occurs when a collection is changing.
  /// </summary>
  event CollectionChangingEventHandler? CollectionChanging;
}
