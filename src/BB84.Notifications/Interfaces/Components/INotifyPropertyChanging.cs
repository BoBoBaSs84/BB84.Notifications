using BB84.Notifications.Components;

namespace BB84.Notifications.Interfaces.Components;

/// <summary>
/// The notify property changing interface.
/// </summary>
public interface INotifyPropertyChanging
{
  /// <summary>
  /// Occurs when the property value is changing.
  /// </summary>
  event PropertyChangingEventHandler? PropertyChanging;
}
