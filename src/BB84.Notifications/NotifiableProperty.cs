// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications;

/// <summary>
/// Represents a property that supports change notifications and can be observed for changes.
/// </summary>
/// <remarks>
/// The <see cref="NotifiableProperty{T}"/> class provides functionality for tracking changes
/// to a property value. It raises <see cref="NotifiablePropertyBase{T}.PropertyChanging"/> and
/// <see cref="NotifiablePropertyBase{T}.PropertyChanged"/> events when the value changes,
/// allowing consumers to react to these changes.
/// </remarks>
/// <typeparam name="T">The type of the property's value.</typeparam>
public sealed class NotifiableProperty<T> : NotifiablePropertyBase<T>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="NotifiableProperty{T}"/> class.
  /// </summary>
  /// <param name="value">The initial value of the property.</param>
  public NotifiableProperty(T value) : base(value)
  { }

  /// <summary>
  /// Initializes a new instance of the <see cref="NotifiableProperty{T}"/> class with an initial
  /// value and a custom equality comparer.
  /// </summary>
  /// <param name="value">The initial value of the property.</param>
  /// <param name="comparer">The equality comparer used to determine whether the value has changed.</param>
  public NotifiableProperty(T value, IEqualityComparer<T> comparer) : base(value, comparer)
  { }

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
  /// value without explicitly accessing the <see cref="NotifiablePropertyBase{T}.Value"/> property.
  /// </remarks>
  /// <param name="property">The <see cref="NotifiableProperty{T}"/> instance to convert.</param>
  public static implicit operator T(NotifiableProperty<T> property)
    => property.Value;
}
