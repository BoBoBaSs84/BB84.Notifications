using BB84.Notifications;
using BB84.Notifications.Interfaces;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class BindablePropertyTests
{
  private sealed class TestClass
  {
    public TestClass(int bindableProperty)
      => BindableProperty = new BindableProperty<int>(bindableProperty);

    public IBindableProperty<int> BindableProperty { get; set; }
  }
}
