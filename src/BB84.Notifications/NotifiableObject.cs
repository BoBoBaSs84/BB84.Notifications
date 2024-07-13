using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

using BB84.Notifications.Attributes;
using BB84.Notifications.Components;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
///	The notifiable object class.
/// </summary>
public abstract class NotifiableObject : INotifiableObject
{
  private static readonly Dictionary<string, string[]> PropertyChangedAttributeCache = [];
  private static readonly Dictionary<string, string[]> PropertyChangingAttributeCache = [];

  /// <summary>
  /// Initializes a new instance of the <see cref="NotifiableObject"/> class.
  /// </summary>
  protected NotifiableObject() => FillCaches();

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
  protected void RaisePropertyChanged(string propertyName)
    => PropertyChanged?.Invoke(this, new(propertyName));

  /// <summary>
  /// Raises the <see cref="PropertyChanged"/> event.
  /// </summary>
  /// <typeparam name="T">The type to work with.</typeparam>
  /// <param name="propertyName">The name of the calling property.</param>
  /// <param name="value">The value of the calling property.</param>
  protected void RaisePropertyChanged<T>(string propertyName, T value)
   => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs<T>(propertyName, value));

  /// <summary>
  /// Raises the <see cref="PropertyChanging"/> event.
  /// </summary>
  /// <param name="propertyName">The name of the calling property.</param>
  protected void RaisePropertyChanging(string propertyName)
    => PropertyChanging?.Invoke(this, new(propertyName));

  /// <summary>
  /// Raises the <see cref="PropertyChanging"/> event.
  /// </summary>
  /// <typeparam name="T">The type to work with.</typeparam>
  /// <param name="propertyName">The name of the calling property.</param>
  /// <param name="value">The value of the calling property.</param>
  protected void RaisePropertyChanging<T>(string propertyName, T value)
    => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs<T>(propertyName, value));

  /// <summary>
  /// Raises the <see cref="PropertyChanged"/> event for all properties that are
  /// defined within the <see cref="NotifyChangedAttribute"/>.
  /// </summary>
  /// <param name="propertyName">The name of the calling property.</param>
  private void RaiseChangedAttribute(string propertyName)
  {
    bool success = PropertyChangedAttributeCache.TryGetValue(propertyName, out string[]? propertiesToNotify);

    if (success && propertiesToNotify is not null)
    {
      foreach (string propertyToNotify in propertiesToNotify)
        RaisePropertyChanged(propertyToNotify);
    }
  }

  /// <summary>
  /// Raises the <see cref="PropertyChanging"/> event for all properties that are
  /// defined within the <see cref="NotifyChangingAttribute"/>.
  /// </summary>
  /// <param name="propertyName">The name of the calling property.</param>
  private void RaiseChangingAttribute(string propertyName)
  {
    bool success = PropertyChangingAttributeCache.TryGetValue(propertyName, out string[]? propertiesToNotify);

    if (success && propertiesToNotify is not null)
    {
      foreach (string propertyToNotify in propertiesToNotify)
        RaisePropertyChanging(propertyToNotify);
    }
  }

  /// <summary>
  /// The method fills the <see cref="PropertyChangedAttributeCache"/> and the <see cref="PropertyChangingAttributeCache"/>
  /// when the class is instantiated so that it only has to do this once. This means that subsequent calls to 
  /// <see cref="RaiseChangedAttribute"/> and <see cref="RaiseChangingAttribute"/> no longer have to do this.
  /// </summary>
  private void FillCaches()
  {
    PropertyInfo[] propertiesInfos = GetType().GetProperties();

    foreach (PropertyInfo propertyInfo in propertiesInfos)
    {
      NotifyChangedAttribute? changedAttribute =
        propertyInfo.GetCustomAttribute(typeof(NotifyChangedAttribute), false) as NotifyChangedAttribute;

      if (changedAttribute is not null)
        PropertyChangedAttributeCache.Add(propertyInfo.Name, changedAttribute.Properties);

      NotifyChangingAttribute? changingAttribute =
        propertyInfo.GetCustomAttribute(typeof(NotifyChangingAttribute), false) as NotifyChangingAttribute;

      if (changingAttribute is not null)
        PropertyChangingAttributeCache.Add(propertyInfo.Name, changingAttribute.Properties);
    }
  }
}
