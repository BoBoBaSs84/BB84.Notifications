using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BB84.Notifications;

/// <summary>
/// The validatable object class.
/// </summary>
public abstract class ValidatableObject : NotificationObject, Interfaces.IValidatableObject
{
  private readonly Dictionary<string, List<string?>> _errors = [];

  /// <inheritdoc/>
  public bool HasErrors => _errors.Count > 0;

  /// <inheritdoc/>
  public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

  /// <inheritdoc/>
  public IEnumerable GetErrors(string propertyName)
  {
    List<string?> values = [];
    return _errors.TryGetValue(propertyName, out values) ? values : Array.Empty<string?>();
  }

  /// <summary>
  /// Sets a new value for a property, notifies about the change and validates the value.
  /// </summary>
  /// <typeparam name="T">The type to work with.</typeparam>
  /// <param name="fieldValue">The referenced field.</param>
  /// <param name="newValue">The new value for the property.</param>
  /// <param name="propertyName">The name of the calling property.</param>
  protected void SetPropertyAndValidate<T>(ref T fieldValue, T newValue, [CallerMemberName] string propertyName = "")
  {
    if (!EqualityComparer<T>.Default.Equals(fieldValue, newValue))
    {
      SetProperty(ref fieldValue, newValue, propertyName);
      Vaildate(newValue, propertyName);
    }
  }
  
  /// <summary>
  /// The method will add an error message for the property.
  /// </summary>
  /// <param name="propertyName">The name of the property.</param>
  /// <param name="errorMessage">The error message for the property.</param>
  protected void AddError(string propertyName, string? errorMessage)
  {
    if (_errors.ContainsKey(propertyName).Equals(false))
      _errors.Add(propertyName, []);

    if (_errors[propertyName].Contains(errorMessage).Equals(false))
      _errors[propertyName].Add(errorMessage);

    RaiseErrorsChanged(propertyName);
  }

  /// <summary>
  /// The method will clear all errors for the property.
  /// </summary>
  /// <param name="propertyName">The name of the property.</param>
  protected void ClearErrors(string propertyName)
  {
    if (_errors.Remove(propertyName))
      RaiseErrorsChanged();
  }

  /// <summary>
  /// The method will raise the <see cref="ErrorsChanged"/> event.
  /// </summary>
  /// <param name="propertyName">The name of the calling property.</param>
  protected void RaiseErrorsChanged([CallerMemberName] string propertyName = "")
    => ErrorsChanged?.Invoke(this, new(propertyName));

  /// <summary>
  /// The method will try to validate the property value.
  /// </summary>
  /// <typeparam name="T">The type to work with.</typeparam>
  /// <param name="value">The value of the proprty.</param>
  /// <param name="propertyName">The name of the property.</param>
  protected void Vaildate<T>(T value, string propertyName)
  {
    ValidationContext context = new(this) { MemberName = propertyName };
    List<ValidationResult> results = [];

    ClearErrors(propertyName);

    if (Validator.TryValidateProperty(value, context, results).Equals(false))
    {
      foreach (ValidationResult result in results)
        AddError(propertyName, result.ErrorMessage);
    }
  }
}
