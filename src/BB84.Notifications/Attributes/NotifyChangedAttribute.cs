// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Attributes;

/// <summary>
/// Indicates that changes to the decorated property should trigger notifications for the specified dependent
/// properties.
/// </summary>
/// <remarks>
/// This attribute is typically used in scenarios where a property change affects other properties, and
/// those dependent properties need to be updated or notified. It is commonly applied in data-binding
/// frameworks to ensure UI elements are refreshed when related properties change.
/// </remarks>
/// <param name="properties">
/// An array of property names that should be notified when the decorated property changes.
/// Each name must correspond to a valid property in the same class.
/// </param>
[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public sealed class NotifyChangedAttribute(params string[] properties) : Attribute
{
  /// <summary>
  /// Gets the collection of property names associated with the current object.
  /// </summary>
  public string[] Properties { get; } = properties;
}
