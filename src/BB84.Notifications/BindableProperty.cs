using System.ComponentModel;

using BB84.Notifications.Components;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
/// The bindable property class.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
/// <remarks>
/// Initializes a instance of the bindable property class.
/// </remarks>
/// <param name="value">The value of the bindable property.</param>
public sealed class BindableProperty<T>(T value) : IBindableProperty<T>
{
  private T _value = value;

  /// <inheritdoc/>
  public bool IsDefault => EqualityComparer<T>.Default.Equals(_value, default!);

  /// <inheritdoc/>
  public bool IsNull => _value is null;

  /// <inheritdoc/>
  public T Value
  {
    get => _value;
    set
    {
      if (!EqualityComparer<T>.Default.Equals(_value, value))
      {
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs<T>(string.Empty, _value));
        _value = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs<T>(string.Empty, value));
      }
    }
  }

  /// <inheritdoc/>
  public event PropertyChangedEventHandler? PropertyChanged;

  /// <inheritdoc/>
  public event PropertyChangingEventHandler? PropertyChanging;
}
