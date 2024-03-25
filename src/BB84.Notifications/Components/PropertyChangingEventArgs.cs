using System.ComponentModel;

namespace BB84.Notifications.Components;

/// <summary>
/// Provides data for the <see cref="INotifyPropertyChanging"/> event.
/// </summary>
/// <typeparam name="T">The type to work with.</typeparam>
/// <param name="propertyName">The name of the property that is changing.</param>
/// <param name="value">The value of the property that is changing.</param>
public sealed class PropertyChangingEventArgs<T>(string? propertyName, T value) : PropertyChangingEventArgs(propertyName)
{
  /// <summary>
  /// Initializes a instance of the <see cref="PropertyChangingEventArgs{T}"/> class.
  /// </summary>
  /// <param name="value">The value of the property that is changing.</param>
  public PropertyChangingEventArgs(T value) : this(null, value)
    => Value = value;

  /// <summary>
  /// The value of the property that is changing.
  /// </summary>
  public T Value { get; } = value;
}
