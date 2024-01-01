namespace BB84.Notifications.Attributes;

/// <summary>
/// The notify changed attribute class.
/// </summary>
/// <remarks>
/// Initializes a instance of the notify changed attribute class.
/// </remarks>
/// <param name="properties">The properties to notify on change.</param>
[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public sealed class NotifyChangedAttribute(params string[] properties) : Attribute
{
  /// <summary>
  /// The properties to notify on change.
  /// </summary>
  public string[] Properties { get; } = properties;
}
