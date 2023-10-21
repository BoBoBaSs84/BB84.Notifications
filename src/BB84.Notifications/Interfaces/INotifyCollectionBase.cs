using BB84.Notifications.Interfaces.Components;

namespace BB84.Notifications.Interfaces;

/// <summary>
/// The notify collection base interface.
/// </summary>
/// <remarks>
///	Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="INotifyCollectionChanging"/> interface</item>
/// <item>The <see cref="INotifyCollectionChanged"/> interface</item>
/// </list>
/// </remarks>
public interface INotifyCollectionBase : INotifyCollectionChanging, INotifyCollectionChanged
{ }
