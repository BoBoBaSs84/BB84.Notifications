using System.ComponentModel;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The validatable object interface.
/// </summary>
public interface IValidatableObject : INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo
{ }
