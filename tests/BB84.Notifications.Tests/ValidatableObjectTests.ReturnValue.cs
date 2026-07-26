// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
using System.ComponentModel.DataAnnotations;

namespace BB84.Notifications.Tests;

public sealed partial class ValidatableObjectTests
{
  [TestMethod]
  public void SetPropertyAndValidateReturnsTrueWhenValueChanged()
  {
    TestClassWithCountingComparer tc = new(new CountingStringComparer());

    Assert.IsTrue(tc.TrySetName("world"));
    Assert.AreEqual("world", tc.Name);
  }

  [TestMethod]
  public void SetPropertyAndValidateReturnsFalseWhenValueUnchanged()
  {
    TestClassWithCountingComparer tc = new(new CountingStringComparer());
    tc.TrySetName("world");

    Assert.IsFalse(tc.TrySetName("world"));
  }

  [TestMethod]
  public void SetPropertyAndValidateConsultsTheComparerExactlyOnce()
  {
    CountingStringComparer comparer = new();
    TestClassWithCountingComparer tc = new(comparer);

    tc.Name = "world";

    // Previously the value was compared once by SetPropertyAndValidate and again
    // by the SetProperty it delegated to.
    Assert.AreEqual(1, comparer.EqualsCallCount);
  }

  [TestMethod]
  public void SetPropertyAndValidateSkipsValidationWhenValueUnchanged()
  {
    CountingStringComparer comparer = new();
    TestClassWithCountingComparer tc = new(comparer);
    tc.Name = new string('x', 51);

    Assert.IsTrue(tc.HasErrors);

    bool raised = false;
    tc.ErrorsChanged += (s, e) => raised = true;

    // Assigning the same invalid value again must not re-run validation.
    tc.Name = new string('x', 51);

    Assert.IsFalse(raised);
  }

  private sealed class TestClassWithCountingComparer(IEqualityComparer<string> comparer) : ValidatableObject
  {
    private string _name = string.Empty;

    [Required, MaxLength(50)]
    public string Name
    {
      get => _name;
      set => SetPropertyAndValidate(ref _name, value, comparer);
    }

    /// <summary>
    /// Surfaces the <c>SetPropertyAndValidate</c> return value, which a property setter cannot.
    /// </summary>
    public bool TrySetName(string value)
      => SetPropertyAndValidate(ref _name, value, comparer, nameof(Name));
  }

  /// <summary>
  /// Counts how often the set pipeline compares values, so the collapsed overloads
  /// cannot silently regress into comparing twice.
  /// </summary>
  private sealed class CountingStringComparer : IEqualityComparer<string>
  {
    public int EqualsCallCount { get; private set; }

    public bool Equals(string? x, string? y)
    {
      EqualsCallCount++;
      return StringComparer.Ordinal.Equals(x, y);
    }

    public int GetHashCode(string obj)
      => StringComparer.Ordinal.GetHashCode(obj);
  }
}
