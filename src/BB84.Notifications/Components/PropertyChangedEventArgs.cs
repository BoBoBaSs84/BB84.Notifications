using System.ComponentModel;

namespace BB84.Notifications.Components;

/// <summary>
/// The property changed event args class.
/// </summary>
/// <typeparam name="T">The type to work with.</typeparam>
/// <remarks>
/// Initializes a instance of the property changed event args class.
/// </remarks>
/// <param name="name">The name of the property that is changed.</param>
/// <param name="value">The value of the property that is changed.</param>
public sealed class PropertyChangedEventArgs<T>(string name, T value) : PropertyChangedEventArgs(name)
{
  /// <summary>
  /// The value of the property that is changed.
  /// </summary>
  public T Value { get; } = value;
}
