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
/// Represents a property that supports reversible changes, allowing navigation
/// through its historical values.
/// </summary>
/// <remarks>
/// This class maintains a history of values up to a specified size, enabling the
/// ability to move forward and backward through the recorded values. It is useful
/// for scenarios where undo/redo functionality or value  tracking is required.
/// </remarks>
/// <typeparam name="T">The type of the property's value.</typeparam>
public sealed class ReversibleProperty<T> : INotifiableProperty<T>, IReversibleProperty<T>
{
  private const int DefaultSize = 10;
  private readonly int _size;
  private readonly List<T> _values;
  private T _value;

  /// <summary>
  /// Initializes a new instance of the <see cref="ReversibleProperty{T}"/> class
  /// with the specified initial value and history size.
  /// </summary>
  /// <param name="value">The initial value of the property.</param>
  /// <param name="size">
  /// The maximum number of previous values to retain in the history.
  /// Defaults to 10 and must be a positive integer.
  /// </param>
  public ReversibleProperty(T value, int size = DefaultSize)
  {
    _size = size;
    _values = new(size);
    _value = value;
    AddValue(value);
  }

  /// <inheritdoc/>
  public int Count => _values.Count;

  /// <inheritdoc/>
  public int Index { get; private set; }

  /// <inheritdoc/>
  public bool IsDefault => EqualityComparer<T>.Default.Equals(_value, default!);

  /// <inheritdoc/>
  public bool IsNull => _value is null;

  /// <inheritdoc/>
  public bool HasNextValue => _values.Count > Index + 1;

  /// <inheritdoc/>
  public bool HasPreviousValue => Index > 0;

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

  /// <inheritdoc/>
  public void NextValue()
  {
    if (!HasNextValue)
      return;

    Index++;
    T value = _values[Index];
    SetProperty(ref _value, value, false);
  }

  /// <inheritdoc/>
  public void PreviousValue()
  {
    if (!HasPreviousValue)
      return;

    Index--;
    T value = _values[Index];
    SetProperty(ref _value, value, false);
  }

  /// <summary>
  /// Implicitly converts a value of type <typeparamref name="T"/> to a
  /// <see cref="ReversibleProperty{T}"/> instance.
  /// </summary>
  /// <param name="value">
  /// The value to be wrapped in a <see cref="ReversibleProperty{T}"/>.
  /// </param>
  public static implicit operator ReversibleProperty<T>(T value)
    => new(value);

  /// <summary>
  /// Implicitly converts a <see cref="ReversibleProperty{T}"/> instance
  /// to its underlying value of type <typeparamref name="T"/>.
  /// </summary>
  /// <param name="property">
  /// The <see cref="ReversibleProperty{T}"/> instance to convert.
  /// </param>
  public static implicit operator T(ReversibleProperty<T> property)
    => property.Value;

  /// <summary>
  /// Updates the value of a property and raises the appropriate change notifications.
  /// </summary>
  /// <remarks>
  /// This method compares the current value and the new value using the default equality comparer
  /// for the type. If the values are not equal, it updates the property value, optionally adds
  /// the new value to an internal collection, and raises <see cref="PropertyChanging"/> and
  /// <see cref="PropertyChanged"/> events.
  /// </remarks>
  /// <param name="oldValue">
  /// A reference to the current value of the property.
  /// This value will be updated to <paramref name="newValue"/> if the values are not equal.
  /// </param>
  /// <param name="newValue">The new value to set for the property.</param>
  /// <param name="addToArray">
  /// A boolean value indicating whether the new value should be added to an internal collection.
  /// <see langword="true"/> to add the new value to the collection; otherwise, <see langword="false"/>.
  /// </param>
  private void SetProperty(ref T oldValue, T newValue, bool addToArray = true)
  {
    if (!EqualityComparer<T>.Default.Equals(oldValue, newValue))
    {
      if (addToArray)
        AddValue(newValue);

      PropertyChanging?.Invoke(this, new PropertyChangingEventArgs<T>(oldValue));
      oldValue = newValue;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs<T>(newValue));
    }
  }

  /// <summary>
  /// Adds a value to the collection, maintaining a fixed maximum size.
  /// </summary>
  /// <remarks>
  /// If the collection has reached its maximum size, the oldest value is removed to make space
  /// for the new value. The <see cref="Index"/> property is updated to reflect the position
  /// of the newly added value.
  /// </remarks>
  /// <param name="value">The value to add to the collection.</param>
  private void AddValue(T value)
  {
    if (_values.Count < _size)
    {
      _values.Add(value);
      Index = _values.LastIndexOf(value);
      return;
    }

    T[] array = new T[_size - 1];
    _values.CopyTo(1, array, 0, _size - 1);
    _values.Clear();
    _values.AddRange(array);
    _values.Add(value);
  }
}
