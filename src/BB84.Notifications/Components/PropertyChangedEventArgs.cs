// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;

namespace BB84.Notifications.Components;

/// <summary>
/// Provides data for the <see cref="INotifyPropertyChanged.PropertyChanged"/> event, including
/// the name of the property that changed and its new value.
/// </summary>
/// <remarks>
/// This class extends <see cref="PropertyChangedEventArgs"/> to include the new value of the
/// property that changed. It is useful for scenarios where the updated value of the property
/// needs to be communicated alongside the property name.
/// </remarks>
/// <typeparam name="T">The type of the property's value.</typeparam>
/// <param name="propertyName">The name of the property that changed.</param>
/// <param name="value">The value of the property that is changed.</param>
public sealed class PropertyChangedEventArgs<T>(string? propertyName, T value) : PropertyChangedEventArgs(propertyName)
{
  /// <summary>
  /// Initializes a new instance of the <see cref="PropertyChangedEventArgs{T}"/> class
  /// with the specified value.
  /// </summary>
  /// <param name="value">The new value associated with the property change.</param>
  public PropertyChangedEventArgs(T value) : this(null, value)
    => Value = value;

  /// <summary>
  /// Gets the value stored in the current instance.
  /// </summary>
  public T Value { get; } = value;
}
