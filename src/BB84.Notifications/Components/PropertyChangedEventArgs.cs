// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;

namespace BB84.Notifications.Components;

/// <summary>
/// Provides data for the <see cref="INotifyPropertyChanged"/> event.
/// </summary>
/// <typeparam name="T">The type to work with.</typeparam>
/// <param name="propertyName">The name of the property that is changed.</param>
/// <param name="value">The value of the property that is changed.</param>
public sealed class PropertyChangedEventArgs<T>(string? propertyName, T value) : PropertyChangedEventArgs(propertyName)
{
  /// <remarks>
  /// Initializes a instance of the <see cref="PropertyChangedEventArgs{T}"/> class.
  /// </remarks>
  /// <param name="value">The value of the property that is changed.</param>
  public PropertyChangedEventArgs(T value) : this(null, value)
    => Value = value;

  /// <summary>
  /// The value of the property that is changed.
  /// </summary>
  public T Value { get; } = value;
}
