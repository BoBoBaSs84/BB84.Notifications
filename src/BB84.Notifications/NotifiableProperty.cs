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
/// Represents a property that supports change notifications and can be observed for changes.
/// </summary>
/// <remarks>
/// The <see cref="NotifiableProperty{T}"/> class provides functionality for tracking changes
/// to a property value. It raises <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/>
/// events when the value changes, allowing consumers to react to these changes.
/// </remarks>
/// <typeparam name="T">The type of the property's value.</typeparam>
/// <param name="value">The initial value of the property.</param>
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
  /// Updates the specified field with a new value and raises property change
  /// notifications if the value changes.
  /// </summary>
  /// <remarks>
  /// This method compares the current value of the field with the new value using the default
  /// equality comparer. If the values are not equal, it raises the <see cref="PropertyChanging"/>
  /// event with the old value, updates the field, and then raises the <see cref="PropertyChanged"/>
  /// event with the new value.
  /// </remarks> 
  /// <param name="fieldValue">A reference to the backing field of the property.</param>
  /// <param name="newValue">The new value to assign to the property.</param>
  private void SetProperty(ref T fieldValue, T newValue)
  {
    if (!EqualityComparer<T>.Default.Equals(fieldValue, newValue))
    {
      PropertyChanging?.Invoke(this, new PropertyChangingEventArgs<T>(fieldValue));
      fieldValue = newValue;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs<T>(newValue));
    }
  }

  /// <summary>
  /// Implicitly converts a value of type <typeparamref name="T"/> to a <see cref="NotifiableProperty{T}"/>.
  /// </summary>
  /// <param name="value">The value to be wrapped in a <see cref="NotifiableProperty{T}"/>.</param>
  public static implicit operator NotifiableProperty<T>(T value)
    => new(value);

  /// <summary>
  /// Implicitly converts a <see cref="NotifiableProperty{T}"/> to its underlying value of type <typeparamref name="T"/>.
  /// </summary>
  /// <remarks>
  /// This operator allows a <see cref="NotifiableProperty{T}"/> to be used directly as its underlying
  /// value without explicitly accessing the <see cref="NotifiableProperty{T}.Value"/> property.
  /// </remarks>
  /// <param name="property">The <see cref="NotifiableProperty{T}"/> instance to convert.</param>
  public static implicit operator T(NotifiableProperty<T> property)
    => property.Value;
}
