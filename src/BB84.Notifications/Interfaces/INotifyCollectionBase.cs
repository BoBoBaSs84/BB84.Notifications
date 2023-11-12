using System.ComponentModel;

using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The notify collection base interface.
/// </summary>
public interface INotifyCollectionBase : INotifyCollectionChanged, INotifyCollectionChanging, INotifyPropertyChanged, INotifyPropertyChanging
{ }
