using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The notifiable collection interface.
/// </summary>
[SuppressMessage("Naming", "CA1711", Justification = "Identifier is correct here")]
public interface INotifiableCollection : INotifyCollectionChanged, INotifyCollectionChanging, INotifyPropertyChanged, INotifyPropertyChanging
{ }
