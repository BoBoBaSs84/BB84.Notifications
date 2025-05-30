// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Attributes;

/// <summary>
/// Indicates that upcoming changes to the decorated property should trigger notifications for the specified
/// dependent properties.
/// </summary>
/// <remarks>
/// Apply this attribute to a property to indicate that changes to the property should notify changes for
/// the specified related properties. This is typically used in scenarios where dependent properties need
/// to be updated or recalculated when the annotated property changes.
/// </remarks>
/// <param name="properties">
/// An array of property names that should be notified when the annotated property changes.
/// Each name must correspond to a valid property in the same class.
/// </param>
[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public sealed class NotifyChangingAttribute(params string[] properties) : Attribute
{
  /// <summary>
  /// The properties to notify on change.
  /// </summary>
  public string[] Properties { get; } = properties;
}
