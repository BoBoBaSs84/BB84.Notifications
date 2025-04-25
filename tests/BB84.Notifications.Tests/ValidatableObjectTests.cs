// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel.DataAnnotations;

namespace BB84.Notifications.Tests;

[TestClass]
public sealed partial class ValidatableObjectTests
{
  private sealed class TestClass(int id, string firstName, string lastName, string? middleName = null) : ValidatableObject
  {
    private int _id = id;
    private string _firstName = firstName;
    private string _lastName = lastName;
    private string? _middleName = middleName;

    public TestClass() : this(1000, string.Empty, string.Empty)
    { }

    [Required, Range(1, int.MaxValue)]
    public int Id
    {
      get => _id;
      set => SetPropertyAndValidate(ref _id, value);
    }

    [Required, MaxLength(50)]
    public string FirstName
    {
      get => _firstName;
      set => SetPropertyAndValidate(ref _firstName, value);
    }

    [MaxLength(50)]
    public string? MiddleName
    {
      get => _middleName;
      set => SetPropertyAndValidate(ref _middleName, value);
    }

    [Required, StringLength(50, MinimumLength = 2)]
    public string LastName
    {
      get => _lastName;
      set => SetPropertyAndValidate(ref _lastName, value);
    }
  }
}
