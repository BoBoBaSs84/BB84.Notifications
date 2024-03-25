using System.ComponentModel;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The notifiable property interface.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
public interface INotifiableProperty<T> : INotifyPropertyChanged, INotifyPropertyChanging
{
  /// <summary>
  /// Is the value of the notifiable property the default value?
  /// </summary>
  bool IsDefault { get; }

  /// <summary>
  /// Is the value of the notifiable property null?
  /// </summary>
  bool IsNull { get; }

  /// <summary>
  /// The actual value of the notifiable property.
  /// </summary>
  T Value { get; set; }
}
