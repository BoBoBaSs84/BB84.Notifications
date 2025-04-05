// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The notifiable object interface.
/// </summary>
public interface INotifiableObject : INotifyPropertyChanged, INotifyPropertyChanging
{ }
