using System.Collections.Generic;

public class AliassesCommand : IConsoleCommand
{
    private readonly ConsoleController _console;

    public AliassesCommand(ConsoleController console)
        => _console = console;

    public string Name => "aliasses";
    public string Description => "Show command aliasses";
    public IEnumerable<string> Aliases => new[] { "alias", "a", "as" };

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            _console.Log("Use: aliasses [command]");
            return;
        }

        var cmd = _console.GetCommand(args[0]);
        if (cmd == null)
        {
            _console.Log($"Command '{args[0]}' not found.");
            return;
        }

        _console.Log($"Aliasses of {cmd.Name}: {string.Join(", ", cmd.Aliases)}");
    }
}