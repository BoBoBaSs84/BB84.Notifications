using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Components;

/// <summary>
/// The property changing event args class.
/// </summary>
public sealed class PropertyChangingEventArgs : EventArgs
{
  /// <summary>
  /// Initializes a instance of the property changing event args class.
  /// </summary>
  /// <param name="name">The name of the property that is changing.</param>
  /// <param name="value">The value of the property that is changing.</param>
  public PropertyChangingEventArgs(string name, object? value = null)
  {
    Name = name;
    Value = value;
  }

  /// <summary>
  /// The name of the property that is changing.
  /// </summary>
  public string Name { get; }

  /// <summary>
  /// The value of the property that is changing.
  /// </summary>
  public object? Value { get; }
}

/// <summary>
/// Represents the method that will handle the event of the <see cref="INotifyPropertyChanging"/> interface.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">The argument that contains the event data.</param>
[SuppressMessage("Naming", "CA1711", Justification = "Event Handler Naming Conventions")]
public delegate void PropertyChangingEventHandler(object? sender, PropertyChangingEventArgs e);
