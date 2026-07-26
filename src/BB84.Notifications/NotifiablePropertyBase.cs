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
/// Provides the shared infrastructure for properties that raise change notifications.
/// </summary>
/// <remarks>
/// This class owns the value, the equality comparer used to detect changes, and both
/// notification events. Derived types add their own behaviour by overriding
/// <see cref="OnValueChanging(T)"/>, which runs after the value is known to have changed
/// but before any notification is raised and can veto the assignment entirely.
/// </remarks>
/// <typeparam name="T">The type of the property's value.</typeparam>
public abstract class NotifiablePropertyBase<T> : INotifiableProperty<T>
{
  private readonly IEqualityComparer<T> _comparer;
  private T _value;

  /// <summary>
  /// Initializes a new instance of the <see cref="NotifiablePropertyBase{T}"/> class.
  /// </summary>
  /// <param name="value">The initial value of the property.</param>
  /// <param name="comparer">
  /// The equality comparer used to determine whether the value has changed.
  /// When <see langword="null"/>, <see cref="EqualityComparer{T}.Default"/> is used.
  /// </param>
  protected NotifiablePropertyBase(T value, IEqualityComparer<T>? comparer = null)
  {
    _comparer = comparer ?? EqualityComparer<T>.Default;
    _value = value;
  }

  /// <inheritdoc/>
  /// <remarks>
  /// This check always uses <see cref="EqualityComparer{T}.Default"/>, independent of the
  /// comparer supplied for change detection.
  /// </remarks>
  public bool IsDefault => EqualityComparer<T>.Default.Equals(_value, default!);

  /// <inheritdoc/>
  public bool IsNull => _value is null;

  /// <inheritdoc/>
  public T Value
  {
    get => _value;
    set => SetValue(value);
  }

  /// <inheritdoc/>
  public event PropertyChangedEventHandler? PropertyChanged;

  /// <inheritdoc/>
  public event PropertyChangingEventHandler? PropertyChanging;

  /// <summary>
  /// Assigns a new value and raises the change notifications when it differs from the current one.
  /// </summary>
  /// <remarks>
  /// The value is compared using the comparer supplied at construction. When it has changed,
  /// <see cref="OnValueChanging(T)"/> is consulted before anything is assigned or raised.
  /// </remarks>
  /// <param name="newValue">The new value to assign.</param>
  /// <returns>
  /// <see langword="true"/> if the value was assigned and notifications were raised;
  /// otherwise, <see langword="false"/>.
  /// </returns>
  protected bool SetValue(T newValue)
  {
    if (_comparer.Equals(_value, newValue))
      return false;

    if (!OnValueChanging(newValue))
      return false;

    RaiseAndAssign(newValue);
    return true;
  }

  /// <summary>
  /// Assigns a new value and raises the change notifications without consulting
  /// <see cref="OnValueChanging(T)"/>.
  /// </summary>
  /// <remarks>
  /// Use this when the derived type is restoring a value it already owns - navigating an
  /// existing history, for example - so that the side effects of
  /// <see cref="OnValueChanging(T)"/> are not applied a second time.
  /// </remarks>
  /// <param name="newValue">The new value to assign.</param>
  /// <returns>
  /// <see langword="true"/> if the value was assigned and notifications were raised;
  /// otherwise, <see langword="false"/>.
  /// </returns>
  protected bool ForceValue(T newValue)
  {
    if (_comparer.Equals(_value, newValue))
      return false;

    RaiseAndAssign(newValue);
    return true;
  }

  /// <summary>
  /// Called when the value has been found to differ, before it is assigned.
  /// </summary>
  /// <remarks>
  /// Derived types override this to attach behaviour to a change, and may return
  /// <see langword="false"/> to abort the assignment so that no notification is raised.
  /// The default implementation accepts every change.
  /// </remarks>
  /// <param name="newValue">The value that is about to be assigned.</param>
  /// <returns>
  /// <see langword="true"/> to proceed with the assignment; <see langword="false"/> to abort it.
  /// </returns>
  protected virtual bool OnValueChanging(T newValue)
    => true;

  /// <summary>
  /// Raises <see cref="PropertyChanging"/> with the outgoing value, assigns the new value and
  /// raises <see cref="PropertyChanged"/> with it.
  /// </summary>
  /// <param name="newValue">The new value to assign.</param>
  private void RaiseAndAssign(T newValue)
  {
    PropertyChanging?.Invoke(this, new PropertyChangingEventArgs<T>(_value));
    _value = newValue;
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs<T>(newValue));
  }
}
