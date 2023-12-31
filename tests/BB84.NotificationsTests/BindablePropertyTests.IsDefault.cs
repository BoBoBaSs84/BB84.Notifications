using BB84.Notifications;
using BB84.Notifications.Interfaces;

namespace BB84.NotificationsTests;

public sealed partial class BindablePropertyTests
{
  [DataTestMethod]
  [DataRow(456, false)]
  [DataRow(6.3d, false)]
  [DataRow(0.75f, false)]
  [DataRow(45623415L, false)]
  [DataRow(0xFE, false)]
  [DataRow(true, false)]
  [DataRow("Test", false)]
  [DataRow(default, true)]
  public void IsDefault(object value, bool expected)
  {
    IBindableProperty<object> bindableProperty = new BindableProperty<object>(value);

    Assert.AreEqual(expected, bindableProperty.IsDefault);
  }

  [DataTestMethod]
  [DataRow(1, false)]
  [DataRow(0, true)]
  [DataRow(default, true)]
  public void IsDefaultInteger(int value, bool expected)
  {
    IBindableProperty<int> bindableProperty = new BindableProperty<int>(value);

    Assert.AreEqual(expected, bindableProperty.IsDefault);
  }

  [DataTestMethod]
  [DataRow(1, false)]
  [DataRow(0, false)]
  [DataRow(default, true)]
  public void IsDefaultNullableInteger(int? value, bool expected)
  {
    IBindableProperty<int?> bindableProperty = new BindableProperty<int?>(value);

    Assert.AreEqual(expected, bindableProperty.IsDefault);
  }
}
