// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

[TestClass]
public sealed partial class NotifiableObjectTests
{
  private sealed class TestClass : NotifiableObject
  {
    private int _property;

    public int Property
    {
      get => _property;
      set => SetProperty(ref _property, value);
    }

    /// <summary>
    /// Surfaces the <c>SetProperty</c> return value, which a property setter cannot.
    /// </summary>
    public bool TrySetProperty(int value)
      => SetProperty(ref _property, value, nameof(Property));

    /// <summary>
    /// Surfaces the return value of the comparer overload.
    /// </summary>
    public bool TrySetProperty(int value, IEqualityComparer<int> comparer)
      => SetProperty(ref _property, value, comparer, nameof(Property));
  }
}
