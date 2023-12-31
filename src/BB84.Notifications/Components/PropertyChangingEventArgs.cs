using System.ComponentModel;

namespace BB84.Notifications.Components;

/// <summary>
/// The property changing event args class.
/// </summary>
/// <typeparam name="T">The type to work with.</typeparam>
/// <remarks>
/// Initializes a instance of the property changing event args class.
/// </remarks>
/// <param name="name">The name of the property that is changing.</param>
/// <param name="value">The value of the property that is changing.</param>
public sealed class PropertyChangingEventArgs<T>(string name, T value) : PropertyChangingEventArgs(name)
{
  /// <summary>
  /// The value of the property that is changing.
  /// </summary>
  public T Value { get; } = value;
}
