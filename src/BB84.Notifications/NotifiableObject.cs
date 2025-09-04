// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

using BB84.Notifications.Attributes;
using BB84.Notifications.Components;
using BB84.Notifications.Interfaces;

namespace BB84.Notifications;

/// <summary>
///	Represents a base class for objects that support property change notifications.
/// </summary>
public abstract class NotifiableObject : INotifiableObject
{
  private readonly Dictionary<string, string[]> _propertiesToNotifyOnChange = [];
  private readonly Dictionary<string, string[]> _propertiesToNotifyOnChanging = [];

  /// <summary>
  /// Initializes a new instance of the <see cref="NotifiableObject"/> class.
  /// </summary>
  protected NotifiableObject() => FillCaches();

  /// <inheritdoc/>
  public event PropertyChangedEventHandler? PropertyChanged;

  /// <inheritdoc/>
  public event PropertyChangingEventHandler? PropertyChanging;

  /// <summary>
  /// Updates the specified field with a new value and raises property change notifications.
  /// </summary>
  /// <remarks>
  /// This method compares the current value of the field with the new value using the default
  /// equality comparer. If the values are not equal, it updates the field and raises property
  /// change notifications. Use this method to implement property setters in classes that
  /// support change tracking or data binding.
  /// </remarks>
  /// <typeparam name="T">The type of the property being updated.</typeparam>
  /// <param name="fieldValue">A reference to the backing field of the property.</param>
  /// <param name="newValue">The new value to assign to the property.</param>
  /// <param name="propertyName">
  /// The name of the property being updated.
  /// This parameter is optional and automatically provided by the compiler if not explicitly specified.
  /// </param>
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
  /// Notifies subscribers that a property value has changed.
  /// </summary>
  /// <remarks>
  /// This method raises the <see cref="PropertyChanged"/> event with the specified property
  /// name and value. Use this method to notify subscribers of changes to property values in
  /// data-binding scenarios.
  /// </remarks>
  /// <param name="propertyName">The name of the property that changed.</param>
  protected void RaisePropertyChanged(string propertyName)
    => PropertyChanged?.Invoke(this, new(propertyName));

  /// <summary>
  /// Notifies subscribers that the values of the specified properties have changed.
  /// </summary>
  /// <remarks>
  /// This method iterates through the provided property names and raises a change notification
  /// for each one by invoking the <see cref="RaisePropertyChanged(string)"/> method.
  /// If no property names are provided, no notifications are raised.
  /// </remarks>
  /// <param name="propertyNames">
  /// An array of property names for which change notifications should be raised. Each name in
  /// the array corresponds to a property for which the change notification will be raised.
  /// </param>
  protected void RaisePropertiesChanged(params string[] propertyNames)
  {
    foreach (string propertyName in propertyNames)
      RaisePropertyChanged(propertyName);
  }

  /// <summary>
  /// Notifies listeners that a property value has changed.
  /// </summary>
  /// <remarks>
  /// This method raises the <see cref="PropertyChanged"/> event with the specified property
  /// name and value. Use this method to notify subscribers of changes to property values in
  /// data-binding scenarios.
  /// </remarks>
  /// <typeparam name="T">The type of the property value.</typeparam>
  /// <param name="propertyName">The name of the property that changed.</param>
  /// <param name="value">The new value of the property.</param>
  protected void RaisePropertyChanged<T>(string propertyName, T value)
   => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs<T>(propertyName, value));

  /// <summary>
  /// Notifies subscribers that a property value is changing.
  /// </summary>
  /// <remarks>
  /// This method raises the <see cref="PropertyChanging"/> event with the specified property
  /// name and value. Use this method to notify subscribers of changes to property values in
  /// data-binding scenarios.
  /// </remarks>
  /// <param name="propertyName">The name of the property is changing.</param>
  protected void RaisePropertyChanging(string propertyName)
    => PropertyChanging?.Invoke(this, new(propertyName));

  /// <summary>
  /// Notifies subscribers that one or more properties are about to change.
  /// </summary>
  /// <remarks>
  /// This method iterates through the provided property names and raises a changing notification
  /// for each one by invoking the <see cref="RaisePropertyChanged(string)"/> method.
  /// If no property names are provided, no notifications are raised.
  /// </remarks>
  /// <param name="propertyNames">
  /// An array of property names that are about to change. Each name in the array corresponds to a
  /// property for which the change notification will be raised.
  /// </param>
  protected void RaisePropertiesChanging(params string[] propertyNames)
  {
    foreach (string propertyName in propertyNames)
      RaisePropertyChanging(propertyName);
  }

  /// <summary>
  /// Notifies subscribers that a property value is changing.
  /// </summary>
  /// <remarks>
  /// This method raises the <see cref="PropertyChanging"/> event with the specified property
  /// name and value. Use this method to notify subscribers of changes to property values in
  /// data-binding scenarios.
  /// </remarks>
  /// <param name="propertyName">The name of the property is changing.</param>
  /// <param name="value">The value of the calling property.</param>
  protected void RaisePropertyChanging<T>(string propertyName, T value)
    => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs<T>(propertyName, value));

  /// <summary>
  /// Notifies listeners that the specified property and its dependent properties have changed.
  /// </summary>
  /// <remarks>
  /// This method checks for dependent properties associated with the specified property name and
  /// raises change notifications for each of them. If no dependencies are found, no additional
  /// notifications are raised.
  /// </remarks>
  /// <param name="propertyName">
  /// The name of the property that has changed. This property must exist in the dependency mapping.
  /// </param>
  private void RaiseChangedAttribute(string propertyName)
  {
    bool success = _propertiesToNotifyOnChange.TryGetValue(propertyName, out string[]? propertiesToNotify);

    if (success && propertiesToNotify is not null)
      RaisePropertiesChanged(propertiesToNotify);
  }

  /// <summary>
  /// Notifies that the specified property is about to change, along with any dependent properties.
  /// </summary>
  /// <remarks>
  /// If the specified property has dependent properties registered in the notification mapping,
  /// this method raises change notifications for those dependent properties as well.
  /// </remarks>
  /// <param name="propertyName">
  /// The name of the property that is changing. This property must exist in the notification mapping.
  /// </param>
  private void RaiseChangingAttribute(string propertyName)
  {
    bool success = _propertiesToNotifyOnChanging.TryGetValue(propertyName, out string[]? propertiesToNotify);

    if (success && propertiesToNotify is not null)
      RaisePropertiesChanging(propertiesToNotify);
  }

  /// <summary>
  /// Populates internal caches with metadata about properties that have:
  /// <list type="bullet">
  /// <item> Properties decorated with <see cref="NotifyChangedAttribute"/></item>
  /// <item> Properties decorated with <see cref="NotifyChangingAttribute"/></item>
  /// </list>
  /// </summary>
  /// <remarks>
  /// For each matching property, it updates internal caches to track the associated metadata.
  /// These caches are used to manage notifications for property changes.
  /// </remarks>
  private void FillCaches()
  {
    PropertyInfo[] propertiesInfos = GetType().GetProperties();

    foreach (PropertyInfo propertyInfo in propertiesInfos)
    {
      NotifyChangedAttribute? changedAttribute =
        propertyInfo.GetCustomAttribute<NotifyChangedAttribute>(false);

      if (changedAttribute is not null)
        _propertiesToNotifyOnChange.Add(propertyInfo.Name, changedAttribute.Properties);

      NotifyChangingAttribute? changingAttribute =
        propertyInfo.GetCustomAttribute<NotifyChangingAttribute>(false);

      if (changingAttribute is not null)
        _propertiesToNotifyOnChanging.Add(propertyInfo.Name, changingAttribute.Properties);
    }
  }
}
