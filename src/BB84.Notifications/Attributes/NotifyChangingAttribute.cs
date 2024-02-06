namespace BB84.Notifications.Attributes;

/// <summary>
/// The notify changing attribute class.
/// </summary>
/// <remarks>
/// Notifies the specified properties about the imminent change to the decorated property.
/// </remarks>
/// <param name="properties">The properties to notify on change.</param>
[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public sealed class NotifyChangingAttribute(params string[] properties) : Attribute
{
  /// <summary>
  /// The properties to notify on change.
  /// </summary>
  public string[] Properties { get; } = properties;
}
