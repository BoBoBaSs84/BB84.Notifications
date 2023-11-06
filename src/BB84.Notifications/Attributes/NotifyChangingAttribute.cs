namespace BB84.Notifications.Attributes;

/// <summary>
/// The notify changing attribute class.
/// </summary>
[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public sealed class NotifyChangingAttribute : Attribute
{
  /// <summary>
  /// Initializes a instance of the notify changing attribute class.
  /// </summary>
  /// <param name="properties">The properties to notify on change.</param>
  /// <exception cref="ArgumentException"></exception>
  public NotifyChangingAttribute(params string[] properties)
  {
    if (properties.Length.Equals(0))
      throw new ArgumentException("Can't be empty.", nameof(properties));

    Properties = properties;
  }

  /// <summary>
  /// The properties to notify on change.
  /// </summary>
  public string[] Properties { get; }
}
