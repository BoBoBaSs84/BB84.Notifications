using System.Reflection;
using System.Runtime.CompilerServices;

using BB84.Notifications.Attributes;
using BB84.Notifications.Components;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
///	The notify property base class.
/// </summary>
/// <remarks>
/// Implements the members of the <see cref="INotifyPropertyBase"/> interface.
/// </remarks>
public abstract class NotifyPropertyBase : INotifyPropertyBase
{
  /// <inheritdoc/>
  public event PropertyChangedEventHandler? PropertyChanged;

  /// <inheritdoc/>
  public event PropertyChangingEventHandler? PropertyChanging;

  /// <summary>
  /// Sets a new value for a property and notifies about the change.
  /// </summary>
  /// <typeparam name="T"></typeparam>
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
  /// <remarks>
  /// The calling member's name will be used as the parameter.
  /// </remarks>
  /// <param name="propertyName">The name of the calling property.</param>
  /// <param name="value">The value of the calling property.</param>
  private void RaisePropertyChanged(string propertyName, object? value = null)
    => PropertyChanged?.Invoke(this, new(propertyName, value));

  /// <summary>
  /// Raises the <see cref="PropertyChanging"/> event.
  /// </summary>
  /// <remarks>
  /// The calling member's name will be used as the parameter.
  /// </remarks>
  /// <param name="propertyName">The name of the calling property.</param>
  /// <param name="value">The value of the calling property.</param>
  private void RaisePropertyChanging(string propertyName, object? value = null)
    => PropertyChanging?.Invoke(this, new(propertyName, value));

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
        foreach (var property in attribute.Properties)
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
        foreach (var property in attribute.Properties)
          RaisePropertyChanging(property);
    }
  }
}
