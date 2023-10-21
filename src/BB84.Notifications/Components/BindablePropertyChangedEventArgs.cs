namespace BB84.Notifications.Components;

/// <summary>
/// The bindable property changed event args class.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
public sealed class BindablePropertyChangedEventArgs<T> : EventArgs where T : IEquatable<T>
{
  /// <summary>
  /// Initializes a instance of the bindable property changed event args class.
  /// </summary>
  /// <param name="value">The value of the property that is changed.</param>
  public BindablePropertyChangedEventArgs(T value)
    => Value = value;

  /// <summary>
  /// The value of the property that is changed.
  /// </summary>
  public T Value { get; }
}
