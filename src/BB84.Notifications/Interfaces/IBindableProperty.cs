using System.ComponentModel;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The bindable property interface.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
public interface IBindableProperty<T> : INotifyPropertyChanged, INotifyPropertyChanging
{
  /// <summary>
  /// Is the value of the bindable property the default value?
  /// </summary>
  bool IsDefault { get; }

  /// <summary>
  /// Is the value of the bindable property null?
  /// </summary>
  bool IsNull { get; }

  /// <summary>
  /// The actual value of the bindable property.
  /// </summary>
  T Value { get; set; }
}
