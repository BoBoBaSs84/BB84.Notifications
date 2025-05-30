// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// Represents an object that provides notifications when its properties are changed or are about to change.
/// </summary>
/// <remarks>
/// This interface combines <see cref="INotifyPropertyChanged"/> and <see cref="INotifyPropertyChanging"/>,
/// enabling consumers to subscribe to events for both property changes and property change initiation.
/// Implementing this interface allows objects to notify observers about changes to their properties,
/// which is useful for data binding and reactive programming scenarios.
/// </remarks>
public interface INotifiableObject : INotifyPropertyChanged, INotifyPropertyChanging
{ }
