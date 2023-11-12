using System.ComponentModel;

using BB84.Notifications.Components;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
/// The bindable property class.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
public sealed class BindableProperty<T> : IBindableProperty<T>
{
  private T _value;

  /// <summary>
  /// Initializes a instance of the bindable property class.
  /// </summary>
  /// <param name="value">The value of the bindable property.</param>
  public BindableProperty(T value)
    => _value = value;

  /// <inheritdoc/>
  public T Value
  {
    get => _value;
    set
    {
      if (!EqualityComparer<T>.Default.Equals(_value, value))
      {
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs<T>(ToString(), _value));
        _value = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs<T>(ToString(), value));
      }
    }
  }

  /// <inheritdoc/>
  public event PropertyChangedEventHandler? PropertyChanged;

  /// <inheritdoc/>
  public event PropertyChangingEventHandler? PropertyChanging;
}
