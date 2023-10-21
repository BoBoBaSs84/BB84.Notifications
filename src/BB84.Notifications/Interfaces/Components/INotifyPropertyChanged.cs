using BB84.Notifications.Components;

namespace BB84.Notifications.Interfaces.Components;

/// <summary>
/// The notify property changed interface.
/// </summary>
public interface INotifyPropertyChanged
{
  /// <summary>
  /// Occurs when the property value is changed.
  /// </summary>
  event PropertyChangedEventHandler? PropertyChanged;
}
