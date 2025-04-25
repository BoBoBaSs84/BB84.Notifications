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
/// The reversible property class.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
public sealed class ReversibleProperty<T> : INotifiableProperty<T>, IReversibleProperty<T>
{
  private readonly int _size;
  private readonly List<T> _values;
  private T _value;

  /// <summary>
  /// Initializes a new instance of the <see cref="ReversibleProperty{T}"/> class.
  /// </summary>
  /// <param name="value">The initial value of the reversible property.</param>
  /// <param name="size">The size of the backing array.</param>
  public ReversibleProperty(T value, int size = 10)
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
  /// Implicitly converts the value type to the <see cref="ReversibleProperty{T}"/>.
  /// </summary>
  /// <param name="value">The value type to convert.</param>
  public static implicit operator ReversibleProperty<T>(T value)
    => new(value);

  /// <summary>
  /// Implicitly converts the <see cref="ReversibleProperty{T}"/> to the value type.
  /// </summary>
  /// <param name="property">The notifiable property to convert.</param>
  public static implicit operator T(ReversibleProperty<T> property)
    => property.Value;

  /// <inheritdoc/>
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
