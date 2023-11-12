﻿using BB84.Notifications.Components;

namespace BB84.NotificationsTests;

public sealed partial class NotifyPropertyBaseTests
{
  [TestMethod]
  public void Changing()
  {
    string changingProperty = string.Empty;
    TestClass testClass = new() { Property = 1 };
    testClass.PropertyChanging += (sender, e) => changingProperty = e.Name;

    testClass.Property = 2;

    Assert.AreEqual(nameof(testClass.Property), changingProperty);
    Assert.AreNotEqual(1, testClass.Property);
  }

  [TestMethod]
  public void ChangingWithValue()
  {
    string propertyName = string.Empty;
    int propertyValue = default;
    TestClass testClass = new() { Property = 1 };
    testClass.PropertyChanging += (sender, e) =>
    {
      if (e is PropertyChangingEventArgs<int> intEvent)
        propertyValue = intEvent.Value;

      propertyName = e.Name;
    };

    testClass.Property = 2;

    Assert.AreEqual(nameof(testClass.Property), propertyName);
    Assert.AreEqual(1, propertyValue);
    Assert.AreEqual(2, testClass.Property);
  }
}
