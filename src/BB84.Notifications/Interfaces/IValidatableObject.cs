// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// Defines an object that supports validation and provides notification of property changes,
/// property-changing events and data error information.
/// </summary>
/// <remarks>
/// Implement this interface to create objects that can be validated for correctness and provide
/// real-time feedback on validation errors. The interface also integrates with property change
/// notifications, making it suitable for use in data-binding scenarios.
/// </remarks>
public interface IValidatableObject : INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo
{
  /// <summary>
  /// Gets a value indicating whether the current state is valid.
  /// </summary>
  /// <remarks>
  /// This is a pure read over the errors recorded so far; it does not trigger validation.
  /// Call <see cref="Validate"/> to (re-)evaluate the object before relying on this value.
  /// </remarks>
  bool IsValid { get; }

  /// <summary>
  /// Validates the whole object and records the resulting validation errors.
  /// </summary>
  /// <remarks>
  /// Any previously recorded errors are discarded, and <see cref="INotifyDataErrorInfo.ErrorsChanged"/>
  /// is raised for every property whose errors were added or removed.
  /// </remarks>
  /// <returns>
  /// <see langword="true"/> if the object passes validation; otherwise, <see langword="false"/>.
  /// </returns>
  bool Validate();
}
