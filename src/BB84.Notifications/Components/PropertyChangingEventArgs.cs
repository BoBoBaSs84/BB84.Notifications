// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;

namespace BB84.Notifications.Components;

/// <summary>
/// Provides data for the event that is raised when a property is changing, including the
/// property's name and its new value.
/// </summary>
/// <remarks>
/// This class extends <see cref="PropertyChangingEventArgs"/> to include the new value
/// of the property. It is typically used in scenarios where the type of the property value
/// is known and needs to be passed along with the property name.
/// </remarks>
/// <typeparam name="T">The type of the value associated with the property that is changing.</typeparam>
/// <param name="propertyName">The name of the property that is changing.</param>
/// <param name="value">The value of the property that is changing.</param>
public sealed class PropertyChangingEventArgs<T>(string? propertyName, T value) : PropertyChangingEventArgs(propertyName)
{
  /// <summary>
  /// Initializes a new instance of the <see cref="PropertyChangingEventArgs{T}"/> class
  /// with the specified value.
  /// </summary>
  /// <param name="value">The new value of the property that is changing.</param>
  public PropertyChangingEventArgs(T value) : this(null, value)
    => Value = value;

  /// <summary>
  /// Gets the value stored in the current instance.
  /// </summary>
  public T Value { get; } = value;
}
