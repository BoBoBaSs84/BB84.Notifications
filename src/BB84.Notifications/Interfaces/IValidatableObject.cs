// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The validatable object interface.
/// </summary>
public interface IValidatableObject : INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo
{
  /// <summary>
  /// Indicates if the object is valid.
  /// </summary>
  bool IsValid { get; }
}
