namespace BB84.NotificationsTests;

public sealed partial class ActionCommandTests
{
  [TestMethod]
  public void RaiseCanExecuteChanged()
  {
    bool commandChanged = default;
    bool intCommandChanged = default;
    TestClass test = new();
    test.Command.CanExecuteChanged += (s, e) => commandChanged = true;
    test.IntCommand.CanExecuteChanged += (s, e) => intCommandChanged = true;

    test.Command.RaiseCanExecuteChanged();
    test.CondCommand.RaiseCanExecuteChanged();
    test.IntCommand.RaiseCanExecuteChanged();
    test.CondIntCommand.RaiseCanExecuteChanged();

    Assert.IsTrue(commandChanged);
    Assert.IsTrue(intCommandChanged);
  }
}
