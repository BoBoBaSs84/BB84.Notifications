// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

public sealed partial class NotifiableObjectTests
{
  [TestMethod]
  public void SetPropertyReturnsTrueWhenValueChanged()
  {
    TestClass testClass = new();

    Assert.IsTrue(testClass.TrySetProperty(42));
    Assert.AreEqual(42, testClass.Property);
  }

  [TestMethod]
  public void SetPropertyReturnsFalseWhenValueUnchanged()
  {
    TestClass testClass = new();
    testClass.TrySetProperty(42);

    Assert.IsFalse(testClass.TrySetProperty(42));
  }

  [TestMethod]
  public void SetPropertyWithComparerReturnsTrueWhenValueChanged()
  {
    TestClass testClass = new();

    Assert.IsTrue(testClass.TrySetProperty(7, EqualityComparer<int>.Default));
  }

  [TestMethod]
  public void SetPropertyWithComparerReturnsFalseWhenValueUnchanged()
  {
    TestClass testClass = new();
    testClass.TrySetProperty(7, EqualityComparer<int>.Default);

    Assert.IsFalse(testClass.TrySetProperty(7, EqualityComparer<int>.Default));
  }

  [TestMethod]
  public void SetPropertyConsultsTheComparerExactlyOnce()
  {
    CountingComparer comparer = new();
    TestClass testClass = new();

    testClass.TrySetProperty(1, comparer);

    Assert.AreEqual(1, comparer.EqualsCallCount);
  }

  /// <summary>
  /// Counts how often the set pipeline compares values, so the collapsed overloads
  /// cannot silently regress into comparing twice.
  /// </summary>
  private sealed class CountingComparer : IEqualityComparer<int>
  {
    public int EqualsCallCount { get; private set; }

    public bool Equals(int x, int y)
    {
      EqualsCallCount++;
      return x == y;
    }

    public int GetHashCode(int obj)
      => obj.GetHashCode();
  }
}
