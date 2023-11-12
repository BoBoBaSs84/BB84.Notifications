using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Components;

/// <summary>
/// The property changing event args class.
/// </summary>
public class PropertyChangingEventArgs : EventArgs
{
  /// <summary>
  /// Initializes a instance of the property changing event args class.
  /// </summary>
  /// <param name="name">The name of the property that is changing.</param>
  public PropertyChangingEventArgs(string name)
    => Name = name;

  /// <summary>
  /// The name of the property that is changing.
  /// </summary>
  public string Name { get; }
}

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

/// <summary>
/// Represents the method that will handle the event of the <see cref="INotifyPropertyChanging"/> interface.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">The argument that contains the event data.</param>
[SuppressMessage("Naming", "CA1711", Justification = "Event Handler Naming Conventions")]
public delegate void PropertyChangingEventHandler(object? sender, PropertyChangingEventArgs e);
