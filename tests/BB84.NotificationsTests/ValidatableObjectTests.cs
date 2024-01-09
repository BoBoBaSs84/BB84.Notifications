using System.ComponentModel.DataAnnotations;

using BB84.Notifications;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class ValidatableObjectTests
{
  private sealed class TestClass : ValidatableObject
  {
    private int _id;
    private string _firstName;
    private string _lastName;
    private string? _middleName;

    public TestClass()
    {
      _id = 1000;
      _firstName = string.Empty;
      _lastName = string.Empty;
      _middleName = default;
    }

    [Required, Range(1, int.MaxValue)]
    public int Id { get => _id; set => SetPropertyAndValidate(ref _id, value); }

    [Required, MaxLength(50)]
    public string FirstName { get => _firstName; set => SetPropertyAndValidate(ref _firstName, value); }

    [MaxLength(50)]
    public string? MiddleName { get => _middleName; set => SetPropertyAndValidate(ref _middleName, value); }

    [Required, StringLength(50, MinimumLength = 2)]
    public string LastName { get => _lastName; set => SetPropertyAndValidate(ref _lastName, value); }
  }
}
