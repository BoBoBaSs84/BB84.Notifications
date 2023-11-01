using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Interfaces;

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

/// <summary>
/// Represents the method that will handle the event of the <see cref="IBindableProperty{T}"/> interface.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
/// <param name="sender">The source of the event.</param>
/// <param name="e">The argument that contains the event data.</param>
[SuppressMessage("Naming", "CA1711", Justification = "Event Handler Naming Conventions")]
public delegate void BindablePropertyChangingEventHandler<T>(object? sender, BindablePropertyChangingEventArgs<T> e) where T : IEquatable<T>;
