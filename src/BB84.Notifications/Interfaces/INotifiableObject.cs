using System.ComponentModel;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The notifiable object interface.
/// </summary>
public interface INotifiableObject : INotifyPropertyChanged, INotifyPropertyChanging
{ }
