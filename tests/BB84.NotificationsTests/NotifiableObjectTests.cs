﻿using BB84.Notifications;

namespace BB84.NotificationsTests;

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
  }
}
