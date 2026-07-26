// Copyright: 2023 Robert Peter Meyer
// License: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
namespace BB84.Notifications.Tests;

public sealed partial class ReversiblePropertyTests
{
  [TestMethod]
  [TestCategory("Method")]
  public void NavigationDoesNotRecordHistory()
  {
    ReversibleProperty<int> property = new(0);
    property.Value = 1;
    property.Value = 2;

    int[] historyAfterAssignments = [.. property.Snapshot()];

    property.PreviousValue();
    property.PreviousValue();
    property.NextValue();

    // Navigating an existing history must neither append to it nor truncate the
    // forward entries. Asserting on Count alone is not enough: re-recording a value
    // that was just truncated can leave the length unchanged while corrupting the
    // contents.
    CollectionAssert.AreEqual(historyAfterAssignments, (System.Collections.ICollection)property.Snapshot());
    Assert.AreEqual(1, property.Value);
  }

  [TestMethod]
  [TestCategory("Method")]
  public void NavigationRaisesNotifications()
  {
    List<int> changed = [];
    ReversibleProperty<int> property = new(0);
    property.Value = 1;
    property.PropertyChanged += (sender, e) =>
    {
      if (e is Components.PropertyChangedEventArgs<int> typed)
        changed.Add(typed.Value);
    };

    property.PreviousValue();
    property.NextValue();

    int[] expected = [0, 1];
    CollectionAssert.AreEqual(expected, changed);
  }

  [TestMethod]
  [TestCategory("Method")]
  public void NavigationDoesNotTruncateForwardHistory()
  {
    ReversibleProperty<int> property = new(0);
    property.Value = 1;
    property.Value = 2;

    property.PreviousValue();

    Assert.IsTrue(property.HasNextValue);
    Assert.AreEqual(3, property.Count);
  }
}
