using System.ComponentModel;

namespace BB84.Notifications.Components;

/// <summary>
/// The property changing event args class.
/// </summary>
/// <typeparam name="T">The type to work with.</typeparam>
public sealed class PropertyChangingEventArgs<T> : PropertyChangingEventArgs
{
  /// <summary>
  /// Initializes a instance of the property changing event args class.
  /// </summary>
  /// <param name="name">The name of the property that is changing.</param>
  /// <param name="value">The value of the property that is changing.</param>
  public PropertyChangingEventArgs(string name, T value) : base(name)
    => Value = value;

  /// <summary>
  /// The value of the property that is changing.
  /// </summary>
  public T Value { get; }
}
