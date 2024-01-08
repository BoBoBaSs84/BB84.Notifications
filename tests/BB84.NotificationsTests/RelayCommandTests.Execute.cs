namespace BB84.NotificationsTests;

public sealed partial class RelayCommandTests
{
  [TestMethod]
  public void Execute()
  {
    TestClass test = new();

    test.Command.Execute(this);

    Assert.AreEqual(1, test.CommandExecution);
  }

  [TestMethod]
  public void ExecuteWithParam()
  {
    TestClass test = new();

    test.IntCommand.Execute((object)1);
    test.IntCommand.Execute(2);

    Assert.AreEqual(2, test.IntegerCommandExecution);
  }
}
