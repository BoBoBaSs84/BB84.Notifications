namespace BB84.NotificationsTests;

public sealed partial class RelayCommandTests
{
  [TestMethod]
  public void CanExecute()
  {
    TestClass test;

    test = new TestClass();

    Assert.IsTrue(test.Command.CanExecute(this));
    Assert.IsFalse(test.CondCommand.CanExecute(this));
  }

  [TestMethod]
  public void CanExecuteWithParam()
  {
    TestClass test;

    test = new TestClass();

    Assert.IsTrue(test.IntCommand.CanExecute(1));
    Assert.IsTrue(test.IntCommand.CanExecute((object)1));
    Assert.IsFalse(test.CondIntCommand.CanExecute(1));
  }
}
