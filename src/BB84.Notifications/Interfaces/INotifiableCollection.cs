// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// Represents a collection that provides notifications when its contents or properties change.
/// </summary>
/// <remarks>This interface combines multiple notification mechanisms, including:
/// <list type="bullet">
/// <item><see cref="INotifyCollectionChanged"/> for changes to the collection's contents.</item>
/// <item><see cref="INotifyCollectionChanging"/> for notifications before changes occur.</item>
/// <item><see cref="INotifyPropertyChanged"/> for changes to the collection's properties.</item>
/// <item><see cref="INotifyPropertyChanging"/> for notifications before property changes occur.</item>
/// </list>
/// Implement this  interface to create collections that support advanced change tracking and notification scenarios.
/// </remarks>
[SuppressMessage("Naming", "CA1711", Justification = "Identifier is correct here")]
public interface INotifiableCollection : INotifyCollectionChanged, INotifyCollectionChanging, INotifyPropertyChanged, INotifyPropertyChanging
{ }
