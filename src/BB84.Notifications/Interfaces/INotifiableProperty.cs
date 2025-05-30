// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// Represents a property that supports change notification and provides additional metadata about its value.
/// </summary>
/// <remarks>
/// This interface extends <see cref="INotifyPropertyChanged"/> and <see cref="INotifyPropertyChanging"/>,
/// enabling consumers to subscribe to notifications when the property's value changes or is about to change.
/// It also provides metadata about the property's value, such as whether it is null or the default value.
/// </remarks>
/// <typeparam name="T">The type of the property's value.</typeparam>
public interface INotifiableProperty<T> : INotifyPropertyChanged, INotifyPropertyChanging
{
  /// <summary>
  /// Gets a value indicating whether the current instance represents the default configuration or state.
  /// </summary>
  bool IsDefault { get; }

  /// <summary>
  /// Gets a value indicating whether the current object represents a null state.
  /// </summary>
  bool IsNull { get; }

  /// <summary>
  /// Gets or sets the value stored in the current instance.
  /// </summary>
  T Value { get; set; }
}
