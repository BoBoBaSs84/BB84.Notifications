using BB84.Notifications;
using BB84.Notifications.Interfaces;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class NotifiablePropertyTests
{
  private sealed class TestClass(int bindableProperty)
  {
    public INotifiableProperty<int> BindableProperty { get; set; } = new NotifiableProperty<int>(bindableProperty);
  }
}
