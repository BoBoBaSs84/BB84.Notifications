// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BB84.Notifications;

/// <summary>
/// Represents an abstract base class for objects that support validation and error notification.
/// </summary>
/// <remarks>
/// This class provides functionality for validating object properties and tracking validation errors.
/// It implements the <see cref="Interfaces.IValidatableObject"/> interface and integrates with data
/// binding scenarios by raising the <see cref="ErrorsChanged"/> event when validation errors change.
/// Derived classes can use the provided methods to validate properties and manage validation errors.
/// </remarks>
public abstract class ValidatableObject : NotifiableObject, Interfaces.IValidatableObject
{
  private readonly Dictionary<string, List<string?>> _errors = [];

  /// <inheritdoc/>
  public bool HasErrors => _errors.Count > 0;

  /// <inheritdoc/>
  public bool IsValid => Validate();

  /// <inheritdoc/>
  public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

#if NET5_0_OR_GREATER
  /// <inheritdoc/>
  public IEnumerable GetErrors(string? propertyName)
    => propertyName is null
    ? _errors
    : _errors.TryGetValue(propertyName, out List<string?>? values) ? values : Array.Empty<string?>();
#else
  /// <inheritdoc/>
  public IEnumerable GetErrors(string propertyName)
    => _errors.TryGetValue(propertyName, out List<string?> values) ? values : Array.Empty<string?>();
#endif

  /// <summary>
  /// Sets the specified property to a new value and performs validation.
  /// </summary>
  /// <remarks>
  /// This method updates the property value only if the new value differs from the current value.
  /// After updating the property, it performs validation using the provided value and property name.
  /// </remarks>
  /// <typeparam name="T">The type of the property value.</typeparam>
  /// <param name="fieldValue">A reference to the backing field of the property.</param>
  /// <param name="newValue">The new value to assign to the property.</param>
  /// <param name="propertyName">
  /// The name of the property being set. This parameter is optional and defaults to the caller's member name.
  /// </param>
  protected void SetPropertyAndValidate<T>(ref T fieldValue, T newValue, [CallerMemberName] string propertyName = "")
  {
    if (!EqualityComparer<T>.Default.Equals(fieldValue, newValue))
    {
      SetProperty(ref fieldValue, newValue, propertyName);
      Vaildate(newValue, propertyName);
    }
  }

  /// <summary>
  /// Raises the <see cref="ErrorsChanged"/> event to notify subscribers that the validation
  /// errors for a specific property have changed.
  /// </summary>
  /// <remarks>
  /// This method is typically used in implementations of the <see cref="INotifyDataErrorInfo"/>
  /// interface to signal changes in validation errors.
  /// </remarks>
  /// <param name="propertyName">
  /// The name of the property whose validation errors have changed. Defaults to the name of
  /// the calling member if not explicitly provided.
  /// </param>
  protected void RaiseErrorsChanged([CallerMemberName] string propertyName = "")
    => ErrorsChanged?.Invoke(this, new(propertyName));

  /// <summary>
  /// Validates the current object based on its data annotations.
  /// </summary>
  /// <remarks>
  /// This method checks the object's properties and fields against the validation attributes
  /// defined in its class. If validation fails, the validation errors are stored for further
  /// inspection.
  /// </remarks>
  /// <returns>
  /// <see langword="true"/> if the object passes validation; otherwise, <see langword="false"/>.
  /// </returns>
  protected bool Validate()
  {
    ValidationContext context = new(this);
    List<ValidationResult> results = [];

    bool isValid = Validator.TryValidateObject(this, context, results, true);

    if (isValid)
      return isValid;

    _errors.Clear();

    foreach (ValidationResult result in results)
    {
      foreach (string propertyName in result.MemberNames)
        AddError(propertyName, result.ErrorMessage);
    }

    return isValid;
  }

  /// <summary>
  /// Validates the specified property value against the validation attributes defined for the property.
  /// </summary>
  /// <remarks>
  /// If the validation fails, any validation errors are added to the error collection for the specified
  /// property. This method clears existing errors for the property before performing validation.
  /// </remarks>
  /// <typeparam name="T">The type of the property value to validate.</typeparam>
  /// <param name="value">The value of the property to validate.</param>
  /// <param name="propertyName">
  /// The name of the property being validated. This must match the name of the property in the class.
  /// </param>
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

  /// <summary>
  /// Adds an error message associated with a specific property name to the error collection.
  /// </summary>
  /// <remarks>
  /// If the specified property does not already exist in the error collection, it is added.
  /// If the error message is not already associated with the property, it is appended to the
  /// list of errors for that property. This method raises the <see cref="RaiseErrorsChanged"/>
  /// event to notify listeners of changes to the error collection.
  /// </remarks>
  /// <param name="propertyName">
  /// The name of the property for which the error is being added. Cannot be null or empty.
  /// </param>
  /// <param name="errorMessage">
  /// The error message to associate with the property. Can be null, in which case no message is added.
  /// </param>
  private void AddError(string propertyName, string? errorMessage)
  {
    if (_errors.ContainsKey(propertyName).Equals(false))
      _errors.Add(propertyName, []);

    if (_errors[propertyName].Contains(errorMessage).Equals(false))
      _errors[propertyName].Add(errorMessage);

    RaiseErrorsChanged(propertyName);
  }

  /// <summary>
  /// Clears all validation errors associated with the specified property.
  /// </summary>
  /// <remarks>
  /// If the specified property has any validation errors, they are removed, and the
  /// <see cref="RaiseErrorsChanged"/> method is invoked to notify listeners of the change.
  /// </remarks>
  /// <param name="propertyName">
  /// The name of the property for which validation errors should be cleared.
  /// </param>
  private void ClearErrors(string propertyName)
  {
    if (_errors.Remove(propertyName))
      RaiseErrorsChanged();
  }
}
