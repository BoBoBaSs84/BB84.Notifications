using BB84.Notifications;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class NotifyPropertyBaseTests
{
  private sealed class TestClass : NotifyPropertyBase
  {
    private int _property;

    public int Property
    {
      get => _property;
      set => SetProperty(ref _property, value);
    }
  }
}
