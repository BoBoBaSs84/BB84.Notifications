using BB84.Notifications.Components;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The bindable property interface.
/// </summary>
public interface IBindableProperty<T> where T : IEquatable<T>
{
  /// <summary>
  /// The actual value of the bindable property.
  /// </summary>
  T Value { get; set; }

  /// <summary>
  /// Occurs when the bindable property value is changed.
  /// </summary>
  public event EventHandler<BindablePropertyChangedEventArgs<T>>? Changed;

  /// <summary>
  /// Occurs when the bindable property value is changing.
  /// </summary>
  public event EventHandler<BindablePropertyChangingEventArgs<T>>? Changing;
}
