// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;

using BB84.Notifications.Components;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
/// The notifiable property class.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
/// <inheritdoc/>
/// <param name="value">The value of the notifiable property.</param>
public sealed class NotifiableProperty<T>(T value) : INotifiableProperty<T>
{
  private T _value = value;

  /// <inheritdoc/>
  public bool IsDefault => EqualityComparer<T>.Default.Equals(_value, default!);

  /// <inheritdoc/>
  public bool IsNull => _value is null;

  /// <inheritdoc/>
  public T Value
  {
    get => _value;
    set => SetProperty(ref _value, value);
  }

  /// <inheritdoc/>
  public event PropertyChangedEventHandler? PropertyChanged;

  /// <inheritdoc/>
  public event PropertyChangingEventHandler? PropertyChanging;

  /// <summary>
  /// Sets the new value the property and notifies about the change.
  /// </summary>
  /// <param name="fieldValue">The referenced value field.</param>
  /// <param name="newValue">The new value for the property.</param>
  private void SetProperty(ref T fieldValue, T newValue)
  {
    if (!EqualityComparer<T>.Default.Equals(fieldValue, newValue))
    {
      PropertyChanging?.Invoke(this, new PropertyChangingEventArgs<T>(fieldValue));
      fieldValue = newValue;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs<T>(newValue));
    }
  }
}
