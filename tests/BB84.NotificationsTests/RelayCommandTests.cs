using BB84.Notifications;
using BB84.Notifications.Interfaces;

namespace BB84.NotificationsTests;

[TestClass]
public sealed partial class RelayCommandTests
{
  private sealed class TestClass
  {
    private IRelayCommand? _command;
    private IRelayCommand? _condCommand;
    private IRelayCommand<int>? _intCommand;
    private IRelayCommand<int>? _condIntCommand;

    public TestClass()
    {
      CommandExecution = default;
      IntegerCommandExecution = default;
    }

    public IRelayCommand Command
      => _command ??= new RelayCommand(x => CommandExecution++);

    public IRelayCommand CondCommand
      => _condCommand ??= new RelayCommand(x => CommandExecution++, x => false);

    public IRelayCommand<int> IntCommand
      => _intCommand ??= new RelayCommand<int>(x => IntegerCommandExecution = x);

    public IRelayCommand<int> CondIntCommand
      => _condIntCommand ??= new RelayCommand<int>(x => IntegerCommandExecution = x, x => false);

    public int CommandExecution { get; private set; }

    public int IntegerCommandExecution { get; private set; }
  }
}
