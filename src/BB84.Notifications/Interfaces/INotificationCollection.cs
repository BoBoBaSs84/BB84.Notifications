using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The notification collection interface.
/// </summary>
[SuppressMessage("Naming", "CA1711", Justification = "Identifier is correct here")]
public interface INotificationCollection : INotifyCollectionChanged, INotifyCollectionChanging, INotifyPropertyChanged, INotifyPropertyChanging
{ }
