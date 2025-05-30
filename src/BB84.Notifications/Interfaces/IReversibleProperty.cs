// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Interfaces;

/// <summary>
/// Represents a property that supports reversible navigation through a sequence of values.
/// </summary>
/// <remarks>
/// This interface extends <see cref="INotifiableProperty{T}"/> to provide functionality for navigating
/// forward and backward through a sequence of values. It includes properties to check the availability
/// of next and previous values, as well as methods to move to the next or previous value.
/// </remarks>
/// <typeparam name="T">The type of the values contained in the property.</typeparam>
public interface IReversibleProperty<T> : INotifiableProperty<T>
{
  /// <summary>
  /// Gets the number of elements contained in the backing array.
  /// </summary>
  int Count { get; }

  /// <summary>
  /// Gets a value indicating whether there is a next value available in the sequence.
  /// </summary>
  bool HasNextValue { get; }

  /// <summary>
  /// Gets a value indicating whether there is a previous value available in the sequence.
  /// </summary>
  bool HasPreviousValue { get; }

  /// <summary>
  /// Gets the zero-based index of the current item.
  /// </summary>
  int Index { get; }

  /// <summary>
  /// Advances the internal state to the next value in the sequence.
  /// </summary>
  /// <remarks>
  /// This method updates the internal state of the sequence, preparing it to return the next value.
  /// It does not return the value itself; instead, it modifies the sequence's state. Call this method
  /// before retrieving the next value from the sequence.
  /// </remarks>
  void NextValue();

  /// <summary>
  /// Retrieves the previous value in a sequence or collection.
  /// </summary>
  /// <remarks>
  /// This method updates the internal state of the sequence, preparing it to return the previous value.
  /// It does not return the value itself; instead, it modifies the sequence's state. Call this method
  /// before retrieving the previous value from the sequence.
  /// </remarks>
  void PreviousValue();
}
