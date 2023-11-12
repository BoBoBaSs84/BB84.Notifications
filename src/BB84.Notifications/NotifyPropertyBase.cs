using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

using BB84.Notifications.Attributes;
using BB84.Notifications.Components;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
///	The notify property base class.
/// </summary>
public abstract class NotifyPropertyBase : INotifyPropertyBase
{
  /// <inheritdoc/>
  public event PropertyChangedEventHandler? PropertyChanged;

  /// <inheritdoc/>
  public event PropertyChangingEventHandler? PropertyChanging;

  /// <summary>
  /// Sets a new value for a property and notifies about the change.
  /// </summary>
  /// <typeparam name="T">The type to work with.</typeparam>
  /// <param name="fieldValue">The referenced field.</param>
  /// <param name="newValue">The new value for the property.</param>
  /// <param name="propertyName">The name of the calling property.</param>
  protected void SetProperty<T>(ref T fieldValue, T newValue, [CallerMemberName] string propertyName = "")
  {
    if (!EqualityComparer<T>.Default.Equals(fieldValue, newValue))
    {
      RaisePropertyChanging(propertyName, fieldValue);
      RaiseChangingAttribute(propertyName);

      fieldValue = newValue;

      RaisePropertyChanged(propertyName, newValue);
      RaiseChangedAttribute(propertyName);
    }
  }

  /// <summary>
  /// Raises the <see cref="PropertyChanged"/> event.
  /// </summary>
  /// <param name="propertyName">The name of the calling property.</param>
  private void RaisePropertyChanged(string propertyName)
    => PropertyChanged?.Invoke(this, new(propertyName));

  /// <summary>
  /// Raises the <see cref="PropertyChanged"/> event.
  /// </summary>
  /// <typeparam name="T">The type to work with.</typeparam>
  /// <param name="propertyName">The name of the calling property.</param>
  /// <param name="value">The value of the calling property.</param>
  private void RaisePropertyChanged<T>(string propertyName, T value)
   => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs<T>(propertyName, value));

  /// <summary>
  /// Raises the <see cref="PropertyChanging"/> event.
  /// </summary>
  /// <param name="propertyName">The name of the calling property.</param>
  private void RaisePropertyChanging(string propertyName)
    => PropertyChanging?.Invoke(this, new(propertyName));

  /// <summary>
  /// Raises the <see cref="PropertyChanging"/> event.
  /// </summary>
  /// <param name="propertyName">The name of the calling property.</param>
  /// <param name="value">The value of the calling property.</param>
  private void RaisePropertyChanging<T>(string propertyName, T value)
    => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs<T>(propertyName, value));

  /// <summary>
  /// Raises the <see cref="PropertyChanged"/> event for all properties that are
  /// defined within the <see cref="NotifyChangedAttribute"/>.
  /// </summary>
  /// <param name="propertyName">The name of the calling property.</param>
  private void RaiseChangedAttribute(string propertyName)
  {
    PropertyInfo? propertyInfo = GetType().GetProperty(propertyName);

    if (propertyInfo is not null)
    {
      NotifyChangedAttribute? attribute =
        propertyInfo.GetCustomAttribute(typeof(NotifyChangedAttribute), false) as NotifyChangedAttribute;

      if (attribute is not null)
        foreach (string property in attribute.Properties)
          RaisePropertyChanged(property);
    }
  }

  /// <summary>
  /// Raises the <see cref="PropertyChanging"/> event for all properties that are
  /// defined within the <see cref="NotifyChangingAttribute"/>.
  /// </summary>
  /// <param name="propertyName">The name of the calling property.</param>
  private void RaiseChangingAttribute(string propertyName)
  {
    PropertyInfo? propertyInfo = GetType().GetProperty(propertyName);

    if (propertyInfo is not null)
    {
      NotifyChangingAttribute? attribute =
        propertyInfo.GetCustomAttribute(typeof(NotifyChangingAttribute), false) as NotifyChangingAttribute;

      if (attribute is not null)
        foreach (string property in attribute.Properties)
          RaisePropertyChanging(property);
    }
  }
}
