using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The notify property base interface.
/// </summary>
/// <remarks>
///	Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="INotifyPropertyChanged"/> interface</item>
/// <item>The <see cref="INotifyPropertyChanging"/> interface</item>
/// </list>
/// </remarks>
public interface INotifyPropertyBase : INotifyPropertyChanged, INotifyPropertyChanging
{ }
