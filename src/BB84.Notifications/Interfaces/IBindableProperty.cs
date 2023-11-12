using System.ComponentModel;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The bindable property interface.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
public interface IBindableProperty<T> : INotifyPropertyChanged, INotifyPropertyChanging
{
  /// <summary>
  /// The actual value of the bindable property.
  /// </summary>
  T Value { get; set; }
}
