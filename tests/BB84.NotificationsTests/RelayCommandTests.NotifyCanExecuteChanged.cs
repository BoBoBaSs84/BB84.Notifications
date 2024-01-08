namespace BB84.NotificationsTests;

public sealed partial class RelayCommandTests
{
  [TestMethod]
  public void NotifyCanExecuteChanged()
  {
    bool commandChanged = default;
    bool intCommandChanged = default;
    TestClass test = new();
    test.Command.CanExecuteChanged += (s, e) => commandChanged = true;
    test.IntCommand.CanExecuteChanged += (s, e) => intCommandChanged = true;

    test.Command.NotifyCanExecuteChanged();
    test.IntCommand.NotifyCanExecuteChanged();

    Assert.IsTrue(commandChanged);
    Assert.IsTrue(intCommandChanged);
  }
}
