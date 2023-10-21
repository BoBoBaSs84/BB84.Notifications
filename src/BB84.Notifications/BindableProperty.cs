using BB84.Notifications.Components;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
/// The bindable property class.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class BindableProperty<T> : IBindableProperty<T> where T : IEquatable<T>
{
  private T _value;

  /// <inheritdoc/>
  public T Value
  {
    get => _value;
    set
    {
      if (EqualityComparer<T>.Default.Equals(_value, value))
        return;

      Changing?.Invoke(this, new BindablePropertyChangingEventArgs<T>(_value));
      _value = value;
      Changed?.Invoke(this, new BindablePropertyChangedEventArgs<T>(value));
    }
  }

  /// <summary>
  /// Initializes a instance of the bindable property class.
  /// </summary>
  /// <param name="value">The value of the bindable property.</param>
  public BindableProperty(T value)
    => _value = value;

  /// <inheritdoc/>	
  public event EventHandler<BindablePropertyChangedEventArgs<T>>? Changed;

  /// <inheritdoc/>
  public event EventHandler<BindablePropertyChangingEventArgs<T>>? Changing;
}
