using System.Runtime.CompilerServices;

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
  protected void SetProperty<T>(ref T fieldValue, T newValue, [CallerMemberName] string propertyName = "") where T : IEquatable<T>
  {
    if (!EqualityComparer<T>.Default.Equals(fieldValue, newValue))
    {
      RaisePropertyChanging(propertyName, fieldValue);
      fieldValue = newValue;
      RaisePropertyChanged(propertyName, newValue);
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
  private void RaisePropertyChanged(string propertyName, object value)
    => PropertyChanged?.Invoke(this, new(propertyName, value));

  /// <summary>
  /// Raises the <see cref="PropertyChanging"/> event.
  /// </summary>
  /// <remarks>
  /// The calling member's name will be used as the parameter.
  /// </remarks>
  /// <param name="propertyName">The name of the calling property.</param>
  /// <param name="value">The value of the calling property.</param>
  private void RaisePropertyChanging(string propertyName, object value)
    => PropertyChanging?.Invoke(this, new(propertyName, value));
}
