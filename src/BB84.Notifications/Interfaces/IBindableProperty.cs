using BB84.Notifications.Components;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The bindable property interface.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
public interface IBindableProperty<T>
{
  /// <summary>
  /// The actual value of the bindable property.
  /// </summary>
  T Value { get; set; }

  /// <summary>
  /// Occurs when the bindable property value is changed.
  /// </summary>
  public event BindablePropertyChangedEventHandler<T>? Changed;

  /// <summary>
  /// Occurs when the bindable property value is changing.
  /// </summary>
  public event BindablePropertyChangingEventHandler<T>? Changing;
}
