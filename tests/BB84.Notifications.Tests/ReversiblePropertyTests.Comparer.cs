// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

public sealed partial class ReversiblePropertyTests
{
  [TestMethod]
  [TestCategory("Comparer")]
  public void SetPropertyWithComparerDoesNotRaiseWhenEqual()
  {
    bool raised = false;
    ReversibleProperty<string> property = new("hello", StringComparer.OrdinalIgnoreCase);
    property.PropertyChanged += (sender, e) => raised = true;

    property.Value = "HELLO";

    Assert.IsFalse(raised);
  }

  [TestMethod]
  [TestCategory("Comparer")]
  public void SetPropertyWithComparerRaisesWhenNotEqual()
  {
    bool raised = false;
    ReversibleProperty<string> property = new("hello", StringComparer.OrdinalIgnoreCase);
    property.PropertyChanged += (sender, e) => raised = true;

    property.Value = "world";

    Assert.IsTrue(raised);
    Assert.AreEqual("world", property.Value);
  }

  [TestMethod]
  [TestCategory("Comparer")]
  public void SetPropertyWithComparerChangingRaisesWhenNotEqual()
  {
    bool raised = false;
    ReversibleProperty<string> property = new("hello", StringComparer.OrdinalIgnoreCase);
    property.PropertyChanging += (sender, e) => raised = true;

    property.Value = "world";

    Assert.IsTrue(raised);
  }

  [TestMethod]
  [TestCategory("Comparer")]
  public void SetPropertyWithComparerDoesNotRecordHistoryWhenEqual()
  {
    ReversibleProperty<string> property = new("hello", StringComparer.OrdinalIgnoreCase);

    property.Value = "HELLO";

    Assert.AreEqual(1, property.Count);
  }

  [TestMethod]
  [TestCategory("Comparer")]
  public void ComparerIsHonouredAlongsideSizeAndOverflow()
  {
    ReversibleProperty<string> property = new("a", StringComparer.OrdinalIgnoreCase, 2, OverflowStrategy.EvictNewest);
    property.Value = "b";

    // Buffer is full, so EvictNewest must reject the assignment.
    property.Value = "c";

    Assert.AreEqual(2, property.Count);
    Assert.AreEqual("b", property.Value);
  }
}
