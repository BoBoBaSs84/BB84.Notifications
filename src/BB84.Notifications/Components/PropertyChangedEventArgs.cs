using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Components;

/// <summary>
/// The property changed event args class.
/// </summary>
public sealed class PropertyChangedEventArgs : EventArgs
{
  /// <summary>
  /// Initializes a instance of the property changed event args class.
  /// </summary>
  /// <param name="name">The name of the property that is changed.</param>
  /// <param name="value">The value of the property that is changed.</param>
  public PropertyChangedEventArgs(string name, object? value = null)
  {
    Name = name;
    Value = value;
  }

  /// <summary>
  /// The name of the property that is changed.
  /// </summary>
  public string Name { get; }

  /// <summary>
  /// The value of the property that is changed.
  /// </summary>
  public object? Value { get; }
}

/// <summary>
/// Represents the method that will handle the event of the <see cref="INotifyPropertyChanged"/> interface.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">The argument that contains the event data.</param>
[SuppressMessage("Naming", "CA1711", Justification = "Event Handler Naming Conventions")]
public delegate void PropertyChangedEventHandler(object? sender, PropertyChangedEventArgs e);
