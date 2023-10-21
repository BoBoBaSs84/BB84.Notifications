namespace BB84.Notifications.Components;

/// <summary>
/// The bindable property changing event args class.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
public class BindablePropertyChangingEventArgs<T> : EventArgs where T : IEquatable<T>
{
  /// <summary>
  /// Initializes a instance of the bindable property changing event args class.
  /// </summary>
  /// <param name="value">The value of the property that is changing.</param>
  public BindablePropertyChangingEventArgs(T value)
    => Value = value;

  /// <summary>
  /// The value of the property that is changing.
  /// </summary>
  public T Value { get; }
}
