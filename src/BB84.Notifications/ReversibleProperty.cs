// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
/// Represents a property that supports reversible changes, allowing navigation
/// through its historical values.
/// </summary>
/// <remarks>
/// This class maintains a history of values up to a specified size, enabling the
/// ability to move forward and backward through the recorded values. It is useful
/// for scenarios where undo/redo functionality or value tracking is required.
/// When the history is full the behaviour is controlled by the <see cref="OverflowStrategy"/>
/// supplied at construction time (default: <see cref="OverflowStrategy.EvictOldest"/>).
/// </remarks>
/// <typeparam name="T">The type of the property's value.</typeparam>
public sealed class ReversibleProperty<T> : NotifiablePropertyBase<T>, IReversibleProperty<T>
{
  private const int DefaultSize = 10;
  private readonly int _size;
  private readonly OverflowStrategy _overflow;
  private readonly List<T> _values;

  /// <summary>
  /// Initializes a new instance of the <see cref="ReversibleProperty{T}"/> class
  /// with the specified initial value, history size, and overflow strategy.
  /// </summary>
  /// <param name="value">The initial value of the property.</param>
  /// <param name="size">
  /// The maximum number of values to retain in the history.
  /// Defaults to 10 and must be a positive integer.
  /// </param>
  /// <param name="overflow">
  /// The strategy to apply when a new value is added and the history is already full.
  /// Defaults to <see cref="OverflowStrategy.EvictOldest"/>.
  /// </param>
  public ReversibleProperty(T value, int size = DefaultSize, OverflowStrategy overflow = OverflowStrategy.EvictOldest)
    : this(value, null, size, overflow)
  { }

  /// <summary>
  /// Initializes a new instance of the <see cref="ReversibleProperty{T}"/> class with the
  /// specified initial value, equality comparer, history size, and overflow strategy.
  /// </summary>
  /// <param name="value">The initial value of the property.</param>
  /// <param name="comparer">
  /// The equality comparer used to determine whether the value has changed.
  /// When <see langword="null"/>, <see cref="EqualityComparer{T}.Default"/> is used.
  /// </param>
  /// <param name="size">
  /// The maximum number of values to retain in the history.
  /// Defaults to 10 and must be a positive integer.
  /// </param>
  /// <param name="overflow">
  /// The strategy to apply when a new value is added and the history is already full.
  /// Defaults to <see cref="OverflowStrategy.EvictOldest"/>.
  /// </param>
  public ReversibleProperty(T value, IEqualityComparer<T>? comparer, int size = DefaultSize, OverflowStrategy overflow = OverflowStrategy.EvictOldest)
    : base(value, comparer)
  {
    _size = size;
    _overflow = overflow;
    _values = new(size);
    _ = TryAddValue(value);
  }

  /// <inheritdoc/>
  public int Count => _values.Count;

  /// <inheritdoc/>
  public int Index { get; private set; }

  /// <inheritdoc/>
  public bool HasNextValue => _values.Count > Index + 1;

  /// <inheritdoc/>
  public bool HasPreviousValue => Index > 0;

  /// <inheritdoc/>
  public void NextValue()
  {
    if (!HasNextValue)
      return;

    Index++;
    ForceValue(_values[Index]);
  }

  /// <inheritdoc/>
  public void PreviousValue()
  {
    if (!HasPreviousValue)
      return;

    Index--;
    ForceValue(_values[Index]);
  }

  /// <inheritdoc/>
  public void Clear()
  {
    _values.Clear();
    _values.Add(Value);
    Index = 0;
  }

  /// <inheritdoc/>
  public IReadOnlyList<T> Snapshot()
    => _values.AsReadOnly();

  /// <summary>
  /// Records the incoming value in the history before it is assigned.
  /// </summary>
  /// <remarks>
  /// Returning <see langword="false"/> aborts the assignment, which is how
  /// <see cref="OverflowStrategy.EvictNewest"/> rejects a value once the buffer is full.
  /// </remarks>
  /// <param name="newValue">The value that is about to be assigned.</param>
  /// <returns>
  /// <see langword="true"/> when the value was recorded and may be assigned;
  /// otherwise, <see langword="false"/>.
  /// </returns>
  protected override bool OnValueChanging(T newValue)
    => TryAddValue(newValue);

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
  /// Attempts to add a value to the history, respecting the configured <see cref="OverflowStrategy"/>.
  /// </summary>
  /// <param name="value">The value to add.</param>
  /// <returns>
  /// <see langword="true"/> if the value was added (or replaced an existing entry);
  /// <see langword="false"/> if the strategy is <see cref="OverflowStrategy.EvictNewest"/> and the
  /// buffer is full — the caller should abort the assignment.
  /// </returns>
  /// <exception cref="InvalidOperationException">
  /// Thrown when the strategy is <see cref="OverflowStrategy.Throw"/> and the buffer is full.
  /// </exception>
  private bool TryAddValue(T value)
  {
    if (_values.Count < _size)
    {
      // If we are not at the end of the history (e.g. after navigating back),
      // truncate the forward entries before recording the new value.
      if (Index < _values.Count - 1)
        _values.RemoveRange(Index + 1, _values.Count - Index - 1);

      _values.Add(value);
      Index = _values.Count - 1;
      return true;
    }

    // Buffer is full — apply overflow strategy.
    switch (_overflow)
    {
      case OverflowStrategy.EvictNewest:
        return false;

      case OverflowStrategy.Throw:
        throw new InvalidOperationException(
          $"The history buffer for {nameof(ReversibleProperty<>)} is full (size = {_size}). " +
          $"Set a larger size or use a different {nameof(OverflowStrategy)}.");

      default: // EvictOldest
        _values.RemoveAt(0);
        _values.Add(value);
        Index = _values.Count - 1;
        return true;
    }
  }
}
