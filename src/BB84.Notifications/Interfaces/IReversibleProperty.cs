// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Interfaces;

/// <summary>
/// The reversible property interface.
/// </summary>
/// <typeparam name="T">The value type to work with.</typeparam>
public interface IReversibleProperty<T> : INotifiableProperty<T>
{
  /// <summary>
  /// Gets the number of elements contained in the backing array.
  /// </summary>
  int Count { get; }

  /// <summary>
  /// Inidcates if there is a next value in the backing store.
  /// </summary>
  bool HasNextValue { get; }

  /// <summary>
  /// Inidcates if there is a previous value in the backing store.
  /// </summary>
  bool HasPreviousValue { get; }

  /// <summary>
  /// The current index of the Value.
  /// </summary>
  int Index { get; }

  /// <summary>
  /// Switches to the next value, if there is a next value.
  /// </summary>
  void NextValue();

  /// <summary>
  /// Switches to the previous value, if there is a next value.
  /// </summary>
  void PreviousValue();
}
